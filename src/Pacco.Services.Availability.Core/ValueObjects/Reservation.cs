using System;

namespace Pacco.Services.Availability.Core.ValueObjects;

public struct Reservation : IEquatable<Reservation>
{
    public Reservation(DateTime dateTime, int priority)
    {
        DateTime = dateTime;
        Priority = priority;
    }

    public DateTime DateTime { get; }
    public int Priority { get; }

    public readonly bool Equals(Reservation other) => DateTime.Equals(other.DateTime) && Priority == other.Priority;

    public override readonly bool Equals(object obj) => obj is Reservation other && Equals(other);

    public override readonly int GetHashCode() => HashCode.Combine(DateTime, Priority);
}
