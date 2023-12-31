﻿using System;

using Convey.CQRS.Events;

namespace Pacco.Services.Availability.Application.Events;

[Contract]
public class ResourceReserved : IEvent
{
    public ResourceReserved(Guid resourceId, DateTime dateTime)
    {
        ResourceId = resourceId;
        DateTime = dateTime;
    }

    public Guid ResourceId { get; }
    public DateTime DateTime { get; }
}
