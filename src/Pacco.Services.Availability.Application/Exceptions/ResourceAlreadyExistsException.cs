using System;

namespace Pacco.Services.Availability.Application.Exceptions;
public class ResourceAlreadyExistsException : AppException
{
    public ResourceAlreadyExistsException(Guid resourceId) : base($"Resource with id: {resourceId} already exists.")
    {
        ResourceId = resourceId;
    }

    public override string Code => "resource_already_exists_exception";

    public Guid ResourceId { get; }
}
