﻿using System.Threading;
using System.Threading.Tasks;

using Convey.CQRS.Queries;

using MongoDB.Driver;

using Pacco.Services.Availability.Application.DTO;
using Pacco.Services.Availability.Application.Queries;
using Pacco.Services.Availability.Infrastructure.Mongo.Documents;

namespace Pacco.Services.Availability.Infrastructure.Mongo.Queries.Handlers;
public class GetResourceHandler : IQueryHandler<GetResource, ResourceDto>
{
    private readonly IMongoDatabase _database;

    public GetResourceHandler(IMongoDatabase database)
    {
        _database = database;
    }
    public async Task<ResourceDto> HandleAsync(GetResource query, CancellationToken cancellationToken = default)
    {
        var document = await _database.GetCollection<ResourceDocument>("resources")
            .Find(r => r.Id == query.ResourceId)
            .SingleOrDefaultAsync(cancellationToken: cancellationToken);

        return document?.AsDto();
    }
}
