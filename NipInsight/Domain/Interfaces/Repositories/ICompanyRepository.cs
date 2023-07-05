using NipInsight.Domain.Entities;

namespace NipInsight.Domain.Interfaces.Repositories;

public interface ICompanyRepository
{
    public Task<Entity> GetByNipAsync(string nip);
    public Task<Entity> AddOrUpdateEntityAsync(Entity entity);
}