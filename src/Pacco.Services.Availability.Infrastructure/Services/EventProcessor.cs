using System.Collections.Generic;
using System.Threading.Tasks;

using Convey.CQRS.Events;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using Pacco.Services.Availability.Application.Events;
using Pacco.Services.Availability.Application.Services;
using Pacco.Services.Availability.Core.Events;

namespace Pacco.Services.Availability.Infrastructure.Services;
internal class EventProcessor : IEventProcessor
{
    private readonly IMessageBroker _messageBroker;
    private readonly IEventMapper _eventMapper;
    private readonly ILogger<EventProcessor> _logger;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public EventProcessor(IMessageBroker messageBroker, IEventMapper eventMapper, ILogger<EventProcessor> logger, IServiceScopeFactory serviceScopeFactory)
    {
        _messageBroker = messageBroker;
        _eventMapper = eventMapper;
        _logger = logger;
        _serviceScopeFactory = serviceScopeFactory;
    }

    public async Task ProcessAsync(IEnumerable<IDomainEvent> events)
    {
        if (events == null)
        {
            return;
        }

        var integrationEvents = await HandleDomainEventsAsync(events);
        if (integrationEvents == null)
        {
            return;
        }

        await _messageBroker.PublishAsync(integrationEvents);
    }

    private async Task<List<IEvent>> HandleDomainEventsAsync(IEnumerable<IDomainEvent> domainEvents)
    {
        var integrationEvents = new List<IEvent>();
        using var scope = _serviceScopeFactory.CreateScope();

        foreach (var domainEvent in domainEvents)
        {
            var domainEventType = domainEvent.GetType();
            _logger.LogTrace($"Handling a domain event: {domainEventType.Name}");
            var handlerType = typeof(IDomainEventHandler<>).MakeGenericType(domainEventType);
            dynamic handlers = scope.ServiceProvider.GetServices(handlerType);

            foreach (var handler in handlers)
            {
                await handler.HadnlerAsync((dynamic)domainEvent);
            }

            var integationEvent = _eventMapper.Map(domainEvent);
            if (integationEvent != null)
            {
                continue;
            }

            integrationEvents.Add(integationEvent);
        }

        return integrationEvents;
    }
}
