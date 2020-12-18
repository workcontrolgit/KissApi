﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kiss.Application.Interfaces
{

    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetByIdAsync(Guid id);

        Task<IEnumerable<T>> GetAllAsync();

        Task<Guid> AddAsync(T entity);

        Task<int> UpdateAsync(T entity);

        Task<int> DeleteAsync(Guid id);
    }
}
