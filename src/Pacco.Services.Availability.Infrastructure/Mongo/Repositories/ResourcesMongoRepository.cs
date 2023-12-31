﻿using System;
using System.Threading.Tasks;

using Convey.Persistence.MongoDB;

using Pacco.Services.Availability.Core.Entities;
using Pacco.Services.Availability.Core.Repositories;
using Pacco.Services.Availability.Infrastructure.Mongo.Documents;

namespace Pacco.Services.Availability.Infrastructure.Mongo.Repositories;

internal sealed class ResourcesMongoRepository : IResourceRepository
{
    private readonly IMongoRepository<ResourceDocument, Guid> _repository;

    public ResourcesMongoRepository(IMongoRepository<ResourceDocument, Guid> repository)
    {
        _repository = repository;
    }

    public Task AddAsync(Resource resource) => _repository.AddAsync(resource.AsDocument());

    public Task DeleteAsync(AggregateId id) => _repository.DeleteAsync(r => r.Id == id);

    public Task<bool> ExistAsync(AggregateId id) => _repository.ExistsAsync(r => r.Id == id);

    public async Task<Resource> GetAsync(AggregateId id)
    {
        var document = await _repository.GetAsync(r => r.Id == id);

        return document?.AsEntity();
    }

    public Task UpdateAsync(Resource resource) => _repository.UpdateAsync(resource.AsDocument());
}
