using AnimalService.Application.Services.AnimalServices;
using AnimalService.Domain.Contracts.Requests.AnimalRequests;
using Microsoft.AspNetCore.Mvc;

namespace AnimalService.Api.Controllers;

[ApiController]
[Route(("api/[controller]"))]
public class AnimalController(IAnimalService service) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateAnimalRequest request)
    {
        var result = await service.Create(request);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] GetAllAnimalsRequest request)
    {
        var result = await service.GetAll(request);
        return Ok(result);    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateAnimalRequest request)
    {
        var result = await service.Update(request);
        return Ok(result);    }
    

    [HttpDelete("{id:int}/soft")]
    public async Task<IActionResult> SoftDelete(int id)
    {
        await service.SoftDelete(id);
        return NoContent();
    }
}