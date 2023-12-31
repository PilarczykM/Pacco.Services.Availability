﻿using System;

using Pacco.Services.Availability.Core.Exceptions;

namespace Pacco.Services.Availability.Core.Entities;
public class AggregateId : IEquatable<AggregateId>
{
    public Guid Value { get; }

    public AggregateId() : this(Guid.NewGuid())
    { }

    public AggregateId(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new InvalidAggregateIdException(value);
        }

        Value = value;
    }

    public bool Equals(AggregateId other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;

        return Value.Equals(other.Value);
    }

    public override bool Equals(object obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;

        return Equals((AggregateId)obj);
    }

    public override int GetHashCode() => Value.GetHashCode();
    public override string ToString() => Value.ToString();

    public static implicit operator Guid(AggregateId id) => id.Value;
    public static implicit operator AggregateId(Guid id) => new(id);
}
