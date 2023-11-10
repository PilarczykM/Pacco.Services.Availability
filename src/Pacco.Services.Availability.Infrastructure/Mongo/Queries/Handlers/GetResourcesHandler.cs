using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Convey.CQRS.Queries;

using MongoDB.Driver;
using MongoDB.Driver.Linq;

using Pacco.Services.Availability.Application.DTO;
using Pacco.Services.Availability.Application.Queries;
using Pacco.Services.Availability.Infrastructure.Mongo.Documents;

namespace Pacco.Services.Availability.Infrastructure.Mongo.Queries.Handlers;

public class GetResourcesHandler : IQueryHandler<GetResources, IEnumerable<ResourceDto>>
{
    private readonly IMongoDatabase _database;

    public GetResourcesHandler(IMongoDatabase database)
    {
        _database = database;
    }

    public async Task<IEnumerable<ResourceDto>> HandleAsync(GetResources query, CancellationToken cancellationToken = default)
    {
        var collection = _database.GetCollection<ResourceDocument>("resources");

        if (query.Tags is null || !query.Tags.Any())
        {
            var allDocuments = await collection.Find(_ => true).ToListAsync(cancellationToken: cancellationToken);

            return allDocuments.Select(d => d.AsDto());
        }

        var documetns = collection.AsQueryable();
        documetns = query.MatchAllTags ? documetns.Where(d => query.Tags.All(t => d.Tags.Contains(t)))
            : documetns.Where(d => query.Tags.Any(t => d.Tags.Contains(t)));

        var resources = await documetns.ToListAsync(cancellationToken: cancellationToken);
        return resources.Select(d => d.AsDto());
    }
}
