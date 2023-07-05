using Microsoft.EntityFrameworkCore;
using NipInsight.Domain.Entities;
using NipInsight.Domain.Interfaces.Repositories;
using NipInsight.Infrastructure.Data;

namespace NipInsight.Infrastructure.Repositories;

public class CompanyRepository : ICompanyRepository
{
    private readonly ApplicationDbContext _context;

    public CompanyRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Entity> GetByNipAsync(string nip) =>
        await _context.Entities.SingleAsync(c => c.Nip == nip);


    public async Task<Entity> AddOrUpdateEntityAsync(Entity entity)
    {
        var existingEntity = await _context.Entities
            .FirstOrDefaultAsync(e => e.Nip == entity.Nip);

        if (existingEntity != null)
        {
            _context.Entry(existingEntity).CurrentValues.SetValues(entity);
            _context.Entry(existingEntity).State = EntityState.Modified;
        }
        else
        {
            await _context.Entities.AddAsync(entity);
        }

        foreach (var person in entity.Representatives!)
        {
            var existingPerson = await _context.EntityPersons
                .FirstOrDefaultAsync(p => p.Id == person.Id);

            if (existingPerson != null)
            {
                _context.Entry(existingPerson).CurrentValues.SetValues(person);
                _context.Entry(existingPerson).State = EntityState.Modified;
            }
            else
            {
                await _context.EntityPersons.AddAsync(person);
            }
        }

        foreach (var person in entity.Partners!)
        {
            var existingPerson = await _context.EntityPersons
                .FirstOrDefaultAsync(p => p.Id == person.Id);

            if (existingPerson != null)
            {
                _context.Entry(existingPerson).CurrentValues.SetValues(person);
                _context.Entry(existingPerson).State = EntityState.Modified;
            }
            else
            {
                await _context.EntityPersons.AddAsync(person);
            }
        }

        foreach (var person in entity.AuthorizedClerks!)
        {
            var existingPerson = await _context.EntityPersons
                .FirstOrDefaultAsync(p => p.Id == person.Id);

            if (existingPerson != null)
            {
                _context.Entry(existingPerson).CurrentValues.SetValues(person);
                _context.Entry(existingPerson).State = EntityState.Modified;
            }
            else
            {
                await _context.EntityPersons.AddAsync(person);
            }
        }

        await _context.SaveChangesAsync();

        return entity;
    }
}