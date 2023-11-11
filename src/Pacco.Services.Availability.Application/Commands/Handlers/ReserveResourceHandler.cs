using System.Threading;
using System.Threading.Tasks;

using Convey.CQRS.Commands;

using Pacco.Services.Availability.Application.Exceptions;
using Pacco.Services.Availability.Core.Repositories;
using Pacco.Services.Availability.Core.ValueObjects;

namespace Pacco.Services.Availability.Application.Commands.Handlers;

public class ReserveResourceHandler : ICommandHandler<ReserveResource>
{
    private readonly IResourceRepository _resourceRepository;

    public ReserveResourceHandler(IResourceRepository resourceRepository)
    {
        _resourceRepository = resourceRepository;
    }

    public async Task HandleAsync(ReserveResource command, CancellationToken cancellationToken = default)
    {
        var resource = await _resourceRepository.GetAsync(command.ResourceId)
            ?? throw new ResourceNotFoundException(command.ResourceId);

        var reservation = new Reservation(command.DateTime, command.Priority);
        resource.AddReservation(reservation);
        await _resourceRepository.UpdateAsync(resource);
    }
}
