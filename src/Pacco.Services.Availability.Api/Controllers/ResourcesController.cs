using System.Collections.Generic;
using System.Threading.Tasks;

using Convey.CQRS.Commands;
using Convey.CQRS.Queries;

using Microsoft.AspNetCore.Mvc;

using Pacco.Services.Availability.Application.Commands;
using Pacco.Services.Availability.Application.DTO;
using Pacco.Services.Availability.Application.Queries;

namespace Pacco.Services.Availability.Api.Controllers;
[ApiController]
[Route("api/[controller]")]
public class ResourcesController : ControllerBase
{
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly IQueryDispatcher _queryDispatcher;

    public ResourcesController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
    {
        _commandDispatcher = commandDispatcher;
        _queryDispatcher = queryDispatcher;
    }

    [HttpGet("")]
    public async Task<ActionResult<List<ResourceDto>>> Get([FromQuery] GetResources query)
    {
        var dtos = await _queryDispatcher.QueryAsync(query);

        if (dtos == null)
        {
            return NotFound();
        }

        return Ok(dtos);
    }

    [HttpGet("{resourceId:guid}")]
    public async Task<ActionResult<ResourceDto>> Get([FromRoute] GetResource query)
    {
        var dto = await _queryDispatcher.QueryAsync(query);
        if (dto == null)
        {
            return NotFound();
        }

        return Ok(dto);
    }

    [HttpPost]
    public async Task<ActionResult> Post(AddResource command)
    {
        await _commandDispatcher.SendAsync(command);

        return Created($"resources/{command.ResourceId}", null);
    }
}
