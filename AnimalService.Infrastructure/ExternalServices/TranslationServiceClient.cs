using System.Data;
using AnimalManagement.Shared.Enums;
using AnimalService.Application.Interfaces.ExternalServices;
using AnimalService.Domain.Contracts.Responses;

namespace AnimalService.Infrastructure.ExternalServices;

public sealed class TranslationServiceClient(HttpClient httpClient) : ITranslationService
{
    public async Task<List<(int, string)>> GetEnumTypeTranslations(
        string enumType, Language language)
    {
        var response = await httpClient.GetFromJsonAsync<GetEnumTypeTranslationsResponse>(
            $"api/translation/{Uri.EscapeDataString(enumType)}/translations/{(int)language}");

        if (response is null)
            throw new NotImplementedException();

        var result = response.EnumTypeTranslations.Select(t => (t.EnumType, t.Translation)).ToList();
        return result;
    }

    public async Task<int> GetEnumCode(string enumType, string translation)
    {
        var response = await httpClient.GetFromJsonAsync<int>(
            $"api/translation/{Uri.EscapeDataString(enumType)}/code?translation={Uri.EscapeDataString(translation)}");

        return response;
    }
}