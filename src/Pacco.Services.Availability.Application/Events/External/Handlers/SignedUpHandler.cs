﻿using System.Threading;
using System.Threading.Tasks;

using Convey.CQRS.Events;

namespace Pacco.Services.Availability.Application.Events.External.Handlers;
public class SignedUpHandler : IEventHandler<SignedUp>
{
    public Task HandleAsync(SignedUp @event, CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }
}
