using System;

using Convey.WebApi.Exceptions;

using Pacco.Services.Availability.Application.Exceptions;
using Pacco.Services.Availability.Core.Exceptions;

namespace Pacco.Services.Availability.Infrastructure.Exceptions;
internal sealed class ExceptionToResponseMapper : IExceptionToResponseMapper
{
    public ExceptionResponse Map(Exception exception)
        => exception switch
        {
            DomainException ex =>
                new ExceptionResponse(new { code = ex.Code, reason = ex.Message }, System.Net.HttpStatusCode.BadRequest),
            AppException ex =>
                new ExceptionResponse(new { code = ex.Code, reason = ex.Message }, System.Net.HttpStatusCode.BadRequest),
            _ => new ExceptionResponse(new { code = "error", reason = "There was an error." }, System.Net.HttpStatusCode.InternalServerError),
        };
}
