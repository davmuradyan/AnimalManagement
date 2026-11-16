using AnimalService.Api.ControllerExtensions;
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
        return this.ErrorOrConverter(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] GetAllAnimalsRequest request)
    {
        var result = await service.GetAll(request);
        return this.ErrorOrConverter(result);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateAnimalRequest request)
    {
        var result = await service.Update(request);
        return this.ErrorOrConverter(result);
    }
    

    [HttpDelete("{id:int}/soft")]
    public async Task<IActionResult> SoftDelete(int id)
    {
        var result = await service.SoftDelete(id);

        if (!result.IsSuccess)
            return this.ErrorOrConverter(result);

        return NoContent();
    }
}