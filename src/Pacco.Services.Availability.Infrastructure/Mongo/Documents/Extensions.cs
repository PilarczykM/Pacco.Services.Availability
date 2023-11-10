using System;
using System.Linq;

using Pacco.Services.Availability.Application.DTO;
using Pacco.Services.Availability.Core.Entities;
using Pacco.Services.Availability.Core.ValueObjects;

namespace Pacco.Services.Availability.Infrastructure.Mongo.Documents;
internal static class Extensions
{
    public static Resource AsEntity(this ResourceDocument document)
        => new(document.Id, document.Tags, document.Reservations
            .Select(r => new Reservation(r.TimeStamp.AsDateTime(), r.Priority)), document.Version);

    public static ResourceDocument AsDocument(this Resource resource)
        => new ResourceDocument()
        {
            Id = resource.Id,
            Tags = resource.Tags,
            Version = resource.Version,
            Reservations = resource.Reservation.Select(r =>
                new ReservationDocument()
                {
                    Priority = r.Priority,
                    TimeStamp = r.DateTime.AsDaysSinceEpoch()
                })
        };

    public static ResourceDto AsDto(this ResourceDocument document)
        => new ResourceDto()
        {
            Id = document.Id,
            Tags = document.Tags,
            Reservations = document.Reservations.Select(r => new ReservationDto()
            {
                DateTime = r.TimeStamp.AsDateTime(),
                Priority = r.Priority
            })
        };


    internal static int AsDaysSinceEpoch(this DateTime dateTime) => (dateTime - new DateTime()).Days;
    internal static DateTime AsDateTime(this int daysSinceEpoch) => new DateTime().AddDays(daysSinceEpoch);
}
