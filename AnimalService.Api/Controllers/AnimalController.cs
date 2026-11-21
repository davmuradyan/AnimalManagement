using AnimalService.Application.Services.AnimalServices;
using AnimalService.Domain.Contracts.Requests.AnimalRequests;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace AnimalService.Api.Controllers;

[ApiController]
[Route(("api/[controller]"))]
public class AnimalController(IAnimalService service) : ControllerBase
{
    [HttpPost]
    [ApiVersion("1.0")]
    public async Task<IActionResult> Create([FromBody] CreateAnimalRequest request)
    {
        var result = await service.Create(request);
        return Ok(result);
    }

    [HttpGet]
    [ApiVersion("1.0")]
    public async Task<IActionResult> GetAll([FromQuery] GetAllAnimalsRequest request)
    {
        var result = await service.GetAll(request);
        return Ok(result);
    }

    [HttpPut]
    [ApiVersion("1.0")]
    public async Task<IActionResult> Update([FromBody] UpdateAnimalRequest request)
    {
        var result = await service.Update(request);
        return Ok(result);
    }

    [HttpDelete("{id:int}/soft")]
    [ApiVersion("1.0")]
    public async Task<IActionResult> SoftDelete(int id)
    {
        await service.SoftDelete(id);
        return NoContent();
    }
}