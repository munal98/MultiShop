using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MultiShop.Order.Application.Interfaces
{
    public interface IRepository<T> where T : class
    {
        // Koleksiyon döndürecek şekilde düzeltilmiş
        Task<IEnumerable<T>> GetAllAsync();

        Task<T> GetByIdAsync(int id); // Tek bir nesne döndüren metot

        Task CreateAsync(T entity); // Yeni bir nesne ekler

        Task UpdateAsync(T entity); // Mevcut bir nesneyi günceller

        Task DeleteAsync(T entity); // Mevcut bir nesneyi siler

        Task<T> GetByFilterAsync(Expression<Func<T, bool>> filter); // Filtre ile tek bir nesne alır
    }
}
