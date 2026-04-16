using Application.Interfaces.Respositories;
using Domain.Commons;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Persistance.DataContexts;


namespace Persistance.Extensions.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseAuditableEntity
{
    private readonly ApplicationDbContext _context;

    public GenericRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<T> CreateAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);

        return entity;
    }
    public IQueryable<T> Entities => _context.Set<T>();

    public async Task DeleteAsync(int id)
    {
        var entity = await _context.Set<T>().FindAsync(id);

        if (entity == null)
        {
            throw new Exception($"Entity with id {id} not found.");
        }
        entity.IsDeleted = true;
        entity.IsActive = false;
        entity.UpdatedDate=DateTime.Now;
        _context.Set<T>().Update(entity);

    }

    public async Task<List<T>> GetAllAsync()
    {
        return await _context.Set<T>().Where(x=>!x.IsDeleted).ToListAsync();
    }

    public async Task<T> GetByIdAsync(int id)
    {
        return await _context.Set<T>().Where(x=>!x.IsDeleted).FirstOrDefaultAsync(x=>x.Id==id);
    }

    public async Task UpdateAsync(T entity)
    {
        var existingEntity = await _context.Set<T>().FindAsync(entity.Id);

        if (existingEntity == null)
        {
            throw new Exception($"Entity with id {entity.Id} not found");
        }

        _context.Set<T>().Update(entity);

    }
}

