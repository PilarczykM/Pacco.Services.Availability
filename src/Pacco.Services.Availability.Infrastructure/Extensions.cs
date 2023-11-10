using System;

using Convey;
using Convey.CQRS.Queries;
using Convey.Persistence.MongoDB;
using Convey.WebApi;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

using Pacco.Services.Availability.Core.Repositories;
using Pacco.Services.Availability.Infrastructure.Exceptions;
using Pacco.Services.Availability.Infrastructure.Mongo.Documents;
using Pacco.Services.Availability.Infrastructure.Mongo.Repositories;

namespace Pacco.Services.Availability.Infrastructure;

public static class Extensions
{
    public static IConveyBuilder AddInfrastructure(this IConveyBuilder builder)
    {
        builder.Services.AddTransient<IResourceRepository, ResourcesMongoRepository>();
        builder.AddQueryHandlers();
        builder.AddInMemoryQueryDispatcher();
        builder.AddErrorHandler<ExceptionToResponseMapper>();
        builder.AddMongo()
            .AddMongoRepository<ResourceDocument, Guid>("resources");

        return builder;
    }

    public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app)
    {
        app
            .UseErrorHandler()
            .UseConvey();

        return app;
    }
}
