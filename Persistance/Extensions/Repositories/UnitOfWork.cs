using Application.Interfaces.Repositories;
using Application.Interfaces.Respositories;
using Domain.Commons;
using Persistance.DataContexts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Persistance.Extensions.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }

    private Hashtable _repository;
    public IGenericRepository<T> Repository<T>() where T : BaseAuditableEntity
    {
        if (_repository == null) { 
        _repository= new Hashtable();
        }
        var type = typeof(T).Name;
        if (!_repository.ContainsKey(type))
        {
            var repositoryType = typeof(GenericRepository<>);
            var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)),_context);
            _repository.Add(type, repositoryInstance);
        }
        return (IGenericRepository<T>)_repository[type];
    }

    public Task<int> Save(CancellationToken cancellationToken)
    {
        return _context.SaveChangesAsync(cancellationToken);
    }
}
