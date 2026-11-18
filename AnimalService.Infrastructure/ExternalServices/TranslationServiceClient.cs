using System.Data;
using AnimalManagement.Shared.Enums;
using AnimalManagement.Shared.Exceptions.Application;
using AnimalService.Application.Interfaces.ExternalServices;
using AnimalService.Domain.Contracts.Responses;
using AnimalService.Domain.Exceptions.Application;

namespace AnimalService.Infrastructure.ExternalServices;

public sealed class TranslationServiceClient(HttpClient httpClient) : ITranslationService
{
    public async Task<List<(int, string)>> GetEnumTypeTranslations(
        string enumType,
        Language language,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var response = await httpClient.GetFromJsonAsync<GetEnumTypeTranslationsResponse>(
                $"api/translation/{Uri.EscapeDataString(enumType)}/translations/{(int)language}",
                cancellationToken);

            if (response is null)
                throw new ExternalServiceException("Unable to retrieve translations. Please try again later.");

            return response.EnumTypeTranslations
                .Select(t => (t.EnumType, t.Translation))
                .ToList();
        }
        catch (ExternalServiceException)
        {
            throw;
        }
        catch (Exception ex) when (ex is HttpRequestException or OperationCanceledException)
        {
            throw new ExternalServiceException("Unable to retrieve translations. Please try again later.", ex);
        }
    }

    public async Task<int> GetEnumCode(
        string enumType,
        string translation,
        CancellationToken cancellationToken = default)
    {
        try
        {
            return await httpClient.GetFromJsonAsync<int>(
                $"api/translation/{Uri.EscapeDataString(enumType)}/code?translation={Uri.EscapeDataString(translation)}",
                cancellationToken);
        }
        catch (Exception ex) when (ex is HttpRequestException or OperationCanceledException)
        {
            throw new ExternalServiceException("Unable to retrieve the translation code. Please try again later.", ex);
        }
    }
}