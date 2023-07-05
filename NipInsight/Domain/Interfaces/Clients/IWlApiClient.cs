using NipInsight.Domain.Entities;

namespace NipInsight.Domain.Interfaces.Clients;

public interface IWlApiClient
{
    public Task<EntityResponse> GetCompanyDataByNipAsync(string nip, DateTime dateTime);
}