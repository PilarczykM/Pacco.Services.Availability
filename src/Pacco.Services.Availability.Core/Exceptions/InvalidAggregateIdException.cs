﻿using System;

namespace Pacco.Services.Availability.Core.Exceptions;
public class InvalidAggregateIdException : DomainException
{
    public override string Code => "invalid_aggregate_id_exception";

    public Guid Id { get; }

    public InvalidAggregateIdException(Guid id) : base($"Invalid aggregate id: {id}")
    {
        Id = id;
    }
}
