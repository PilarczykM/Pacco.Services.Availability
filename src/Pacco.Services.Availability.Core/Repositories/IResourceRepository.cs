using System.Threading.Tasks;

using Pacco.Services.Availability.Core.Entities;

namespace Pacco.Services.Availability.Core.Repositories;
public interface IResourceRepository
{
    Task<Resource> GetAsync(AggregateId id);
    Task<bool> ExistAsync(AggregateId id);
    Task AddAsync(Resource resource);
    Task UpdateAsync(Resource resource);
    Task DeleteAsync(AggregateId id);
}
