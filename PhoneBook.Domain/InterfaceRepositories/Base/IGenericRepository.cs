using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhoneBook.Domain.Models.Base;

namespace PhoneBook.Domain.InterfaceRepositories.Base
{
    public interface IGenericRepository<TEntity> : IAsyncDisposable where TEntity : BaseEntity
    {
        IQueryable<TEntity> GetQuery();
        Task AddEntity(TEntity entity);
        Task AddRangeEntities(List<TEntity> entities);
        Task<TEntity> GetEntityById(long entityId);
        void EditEntity(TEntity entity);
        void DeleteEntity(TEntity entity);
        Task DeleteEntity(long entityId);
        void DeletePermanent(TEntity entity);
        Task DeletePermanent(long entityId);
        Task SaveChanges();
    }
}
