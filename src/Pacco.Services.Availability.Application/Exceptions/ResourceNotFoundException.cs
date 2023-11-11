using System;

namespace Pacco.Services.Availability.Application.Exceptions;

public class ResourceNotFoundException : AppException
{
    public ResourceNotFoundException(Guid resourceId) : base($"Resource with id {resourceId} not found.")
    {
        ResourceId = resourceId;
    }

    public override string Code => "resource_not_found_exception";

    public Guid ResourceId { get; }
}