using System;

using Convey.CQRS.Events;

namespace Pacco.Services.Availability.Application.Events.Rejected;

[Contract]
public class AddResourceRejected : IRejectedEvent
{
    public AddResourceRejected(Guid resourceId, string reason, string code)
    {
        ResourceId = resourceId;
        Reason = reason;
        Code = code;
    }

    public Guid ResourceId { get; }
    public string Reason { get; }
    public string Code { get; }
}
