using System;

using Convey.CQRS.Events;

namespace Pacco.Services.Availability.Application.Events;
public class ResourceReservationCanceled : IEvent
{
    public ResourceReservationCanceled(Guid resourceId, DateTime dateTime)
    {
        ResourceId = resourceId;
        DateTime = dateTime;
    }

    public Guid ResourceId { get; }
    public DateTime DateTime { get; }

}
