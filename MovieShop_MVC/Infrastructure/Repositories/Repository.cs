using Infrastructure.Data;
using ApplicationCore.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly MovieShopDbContext _dbContext;

        public Repository( MovieShopDbContext dbContext)
        {
            _dbContext = dbContext ;
        }

        public virtual async Task<T> Add(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }
        public virtual async Task<T> Delete(T entity)
        {
            throw new NotImplementedException();
        }
        public virtual async Task<IEnumerable<T>>  GetAll()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }
        public async virtual Task<T> GetById(int id)
        {
            
            return await _dbContext.Set<T>().FindAsync(id);
        }
        public virtual async Task<T> Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
