using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestExecution.Data.Contexts;
using TestExecution.Data.IRepositories;
using TestExecution.Domain.Commons;

namespace TestExecution.Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : Auditable
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<T> CreateAsync(T entity)
        {
            entity.CreatedAt = DateTime.UtcNow;
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var result = await _context.Set<T>()
                .Where(x => x.Id == id && x.IsDeleted == false).FirstOrDefaultAsync();
            result.IsDeleted = true;
            result.DeletedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return true;
        }

        public IQueryable<T> GetAll()
        {
            var result =  _context.Set<T>().Where(x=>x.IsDeleted == false).AsQueryable();
            return result;
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            var result = await _context.Set<T>()
                .Where(x => x.Id == id && x.IsDeleted == false).FirstOrDefaultAsync();
            return result;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            entity.UpdatedAt = DateTime.UtcNow;
            var result = _dbSet.Update(entity);
            await _context.SaveChangesAsync();
            return result.Entity;
        }
    }
}
