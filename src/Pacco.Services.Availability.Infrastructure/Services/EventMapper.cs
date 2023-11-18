using System.Collections.Generic;
using System.Linq;

using Convey.CQRS.Events;

using Pacco.Services.Availability.Application.Events;
using Pacco.Services.Availability.Application.Services;
using Pacco.Services.Availability.Core.Events;

namespace Pacco.Services.Availability.Infrastructure.Services;
internal class EventMapper : IEventMapper
{
    public IEvent Map(IDomainEvent @event)
        => @event switch
        {
            ResourceCreated e => new ResourceAdded(e.Resource.Id),
            ReservationCanceled e => new ResourceReservationCanceled(e.Resource.Id, e.Reservation.DateTime),
            ReservationAdded e => new ResourceReserved(e.Resource.Id, e.Reservation.DateTime),
            _ => null,
        };

    public IEnumerable<IEvent> MapAll(IEnumerable<IDomainEvent> events)
        => events.Select(Map);
}
