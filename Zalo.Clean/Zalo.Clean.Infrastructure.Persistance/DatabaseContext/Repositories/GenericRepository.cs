using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zalo.Clean.Application.Contracts.Persistance;
using Zalo.Clean.Domain.Common;

namespace Zalo.Clean.Infrastructure.Persistance.DatabaseContext.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        protected readonly ZaloDatabaseContext context;

        public GenericRepository(ZaloDatabaseContext context)
        {
            this.context = context;
        }

        public async Task<T> CreateAsync(T entity)
        {
            await context.AddAsync(entity);

            await context.SaveChangesAsync();

            return entity;
        }

        public async Task<T> DeleteAsync(T entity)
        {
            context.Remove(entity);

            await context.SaveChangesAsync();

            return entity;
        }

        public async Task<IReadOnlyList<T>> GetAsync()
        {
            return await context.Set<T>().AsNoTracking().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await context.Set<T>().AsNoTracking().FirstOrDefaultAsync(q =>q.Id == id);
        }

        public async Task<T> UpdateAsync(T entity)
        {
            context.Update(entity);
            context.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return entity;
        }
    }
}
