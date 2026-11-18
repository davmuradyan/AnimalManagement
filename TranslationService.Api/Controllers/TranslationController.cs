using Microsoft.AspNetCore.Mvc;
using TranslationService.Application.Services.TranslationServices;

namespace TranslationService.Api.Controllers;

[ApiController]
[Route(("api/[controller]"))]
public class TranslationController(ITranslationService service) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllTranslations(int language)
    {
        var result = await service.GetAllTranslations(language);

        return Ok(result);
    }

    [HttpGet("{enumType}/translations/{language}")]
    public async Task<IActionResult> GetEnumTypeTranslations(
        string enumType, int language)
    {
        var result = await service.GetEnumTypeTranslations(enumType, language);
        return Ok(result);
    }

    [HttpGet("{enumType}/code")]
    public async Task<IActionResult> GetEnumCode(string enumType, [FromQuery] string translation)
    {
        var result = await service.GetEnumCode(enumType, translation);
        return Ok(result);
    }
}