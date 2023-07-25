using Microsoft.EntityFrameworkCore;
using PhoneBook.Domain.InterfaceRepositories.Base;
using PhoneBook.Domain.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhoneBook.Infrastructure.EFCore.Context;

namespace PhoneBook.Infrastructure.EFCore.Repository.Base
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly PhoneBookDbContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public GenericRepository(PhoneBookDbContext context)
        {
            _context = context;
            this._dbSet = _context.Set<TEntity>();
        }

        public IQueryable<TEntity> GetQuery()
        {
            return _dbSet.AsQueryable();
        }

        public async Task AddEntity(TEntity entity)
        {

            await _dbSet.AddAsync(entity);
        }

        public async Task AddRangeEntities(List<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                await AddEntity(entity);
            }
        }

        public async Task<TEntity> GetEntityById(long entityId)
        {
            return await _dbSet.SingleOrDefaultAsync(t => t.Id == entityId);
        }

        public void EditEntity(TEntity entity)
        {

            _dbSet.Update(entity);
        }

        public void DeleteEntity(TEntity entity)
        {

            EditEntity(entity);
        }

        public async Task DeleteEntity(long entityId)
        {
            TEntity entity = await GetEntityById(entityId);
            if (entity != null) DeleteEntity(entity);
        }

        public void DeletePermanent(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        public async Task DeletePermanent(long entityId)
        {
            TEntity entity = await GetEntityById(entityId);
            if (entity != null) DeletePermanent(entity);
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

        public async ValueTask DisposeAsync()
        {
            if (_context != null)
            {
                await _context.DisposeAsync();
            }
        }
    }
}
