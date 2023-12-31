﻿using System;
using System.Collections.Generic;

using Convey.CQRS.Commands;

namespace Pacco.Services.Availability.Application.Commands;
public class AddResource : ICommand
{
    public AddResource(Guid resourceId, IEnumerable<string> tags)
    {
        ResourceId = resourceId;
        Tags = tags;
    }

    public Guid ResourceId { get; }
    public IEnumerable<string> Tags { get; }
}
