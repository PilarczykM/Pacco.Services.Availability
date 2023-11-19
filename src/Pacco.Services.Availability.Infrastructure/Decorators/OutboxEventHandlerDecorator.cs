using System;
using System.Threading;
using System.Threading.Tasks;

using Convey.CQRS.Events;
using Convey.MessageBrokers;
using Convey.MessageBrokers.Outbox;

namespace Pacco.Services.Availability.Infrastructure.Decorators;
internal class OutboxEventHandlerDecorator<T> : IEventHandler<T> where T : class, IEvent
{
    private readonly IEventHandler<T> _handler;
    private readonly IMessageOutbox _outbox;
    private readonly bool _enabled;
    private readonly string _messageId;

    public OutboxEventHandlerDecorator(IEventHandler<T> handler, IMessageOutbox outbox, OutboxOptions outboxOptions,
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
    public Task HandleAsync(T @event, CancellationToken cancellationToken = default)
        => _enabled
            ? _outbox.HandleAsync(_messageId, () => _handler.HandleAsync(@event, cancellationToken))
            : _handler.HandleAsync(@event, cancellationToken);
}
