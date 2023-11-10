using System;

namespace Pacco.Services.Availability.Application.Exceptions;
public class ResourceAlreadyExistsException : AppException
{
    public ResourceAlreadyExistsException(Guid id) : base($"Resource with id: {id} already exists.")
    {
        Id = id;
    }

    public override string Code => "resource_already_exists_exception";

    public Guid Id { get; }
}
