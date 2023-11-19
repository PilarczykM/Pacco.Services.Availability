using System;
using System.Threading;
using System.Threading.Tasks;

using Convey.CQRS.Commands;
using Convey.MessageBrokers;
using Convey.MessageBrokers.Outbox;

namespace Pacco.Services.Availability.Infrastructure.Decorators;
internal class OutboxCommandHandlerDecorator<T> : ICommandHandler<T> where T : class, ICommand
{
    private readonly ICommandHandler<T> _handler;
    private readonly IMessageOutbox _outbox;
    private readonly bool _enabled;
    private readonly string _messageId;

    public OutboxCommandHandlerDecorator(ICommandHandler<T> handler, IMessageOutbox outbox, OutboxOptions outboxOptions,
        IMessagePropertiesAccessor messagePropertiesAccessor)
    {
        _handler = handler;
        _outbox = outbox;
        _enabled = outboxOptions.Enabled;
        var messageProperties = messagePropertiesAccessor.MessageProperties;
        _messageId = string.IsNullOrWhiteSpace(messageProperties?.MessageId)
            ? Guid.NewGuid().ToString("N")
            : messageProperties.MessageId;
    }
    public Task HandleAsync(T command, CancellationToken cancellationToken = default)
        => _enabled
            ? _outbox.HandleAsync(_messageId, () => _handler.HandleAsync(command, cancellationToken))
            : _handler.HandleAsync(command, cancellationToken);
}
