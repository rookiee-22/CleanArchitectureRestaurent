using Domain.Commons;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces.Respositories;

public interface IGenericRepository<T> where T : BaseAuditableEntity
{
    Task<T> CreateAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(int id);
    Task<T> GetByIdAsync(int id);
    Task<List<T>> GetAllAsync();
}
