﻿namespace DataAccessLayer.Interfaces;
public interface IRepository<T>
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetByIdAsync(int id);
    T Add(T entity);
    void Update(T entity);
    void Delete(int id);
}