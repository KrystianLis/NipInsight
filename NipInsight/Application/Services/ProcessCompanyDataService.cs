using NipInsight.Application.Common;
using NipInsight.Application.DTOs;
using NipInsight.Domain.Extensions;
using NipInsight.Domain.Interfaces.Clients;
using NipInsight.Domain.Interfaces.Repositories;
using NipInsight.Domain.Interfaces.Services;

namespace NipInsight.Application.Services;

public class ProcessCompanyDataService : IProcessCompanyDataService
{
    private readonly ICompanyRepository _companyRepository;
    private readonly IWlApiClient _httpClient;

    public ProcessCompanyDataService(IWlApiClient httpClient, ICompanyRepository companyRepository)
    {
        _httpClient = httpClient;
        _companyRepository = companyRepository;
    }

    public async Task<OperationResult<CompanyDto>> GetAndStoreCompanyData(string nip, DateTime dateTime)
    {
        if (!nip.IsValidNip())
        {
            return new OperationResult<CompanyDto>
            {
                Success = false,
                ErrorMessage = "Nip is not valid"
            };
        }

        var data = await _httpClient.GetCompanyDataByNipAsync(nip, dateTime);

        if (data.Exception is not null)
        {
            return new OperationResult<CompanyDto>
            {
                Success = false,
                ErrorMessage = $"HTTP error code: {data.Exception.Code}. Message: {data.Exception.Message}"
            };
        }

        var companyData = await _companyRepository.AddOrUpdateEntityAsync(data.Result.Subject);

        return new OperationResult<CompanyDto>
        {
            Success = true,
            Result = new CompanyDto
            {
                Name = companyData.Name,
                Nip = companyData.Nip,
                Regon = companyData.Regon,
                ResidenceAddress = companyData.ResidenceAddress
            }
        };
    }
}