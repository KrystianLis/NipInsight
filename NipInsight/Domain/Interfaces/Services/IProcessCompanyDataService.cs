using NipInsight.Application.Common;
using NipInsight.Application.DTOs;

namespace NipInsight.Domain.Interfaces.Services;

public interface IProcessCompanyDataService
{
    public Task<OperationResult<CompanyDto>> GetAndStoreCompanyData(string nip, DateTime dateTime);
}