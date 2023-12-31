﻿using System;

using Convey.CQRS.Commands;

namespace Pacco.Services.Availability.Application.Commands;
public class ReserveResource : ICommand
{
    public ReserveResource(Guid resourceId, DateTime dateTime, int priority)
    {
        ResourceId = resourceId;
        DateTime = dateTime;
        Priority = priority;
    }

    public Guid ResourceId { get; }
    public DateTime DateTime { get; }
    public int Priority { get; }
}
