﻿using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories;
public class Repository<T>(AppDbContext dbContext)
    : IRepository<T> where T : BaseEntity
{
    protected readonly AppDbContext _dbContext = dbContext;
    private readonly DbSet<T> _entities = dbContext.Set<T>();

    public T Add(T entity)
    {
        _entities.Add(entity);
        return entity;
    }

    public void Delete(int id)
    {
        var entity = _entities.FirstOrDefault(i => i.Id == id);
        if (entity == null)
        {
            return;
        }
        _entities.Remove(entity);
    }

    public async Task<IEnumerable<T>> GetAllAsync()
        => await _entities.ToListAsync();

    public async Task<T?> GetByIdAsync(int id)
        => await _entities.FirstOrDefaultAsync(i => i.Id == id);

    public void Update(T entity)
        => _entities.Update(entity);
}