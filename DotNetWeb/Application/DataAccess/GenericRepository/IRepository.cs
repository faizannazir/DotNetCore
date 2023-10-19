using DataAccess.Entities.BaseEntity;

namespace DataAccess.GenericRepository
{
    public interface IRepository<TEntity> where TEntity : class
    {

        IQueryable<TEntity> GetAll();
        IQueryable<TEntity> GetReadOnlyList();
        IQueryable<TEntity> GetNotDeleted();
        Task<TEntity> Get<T>(T id);
        void SetAdded(TEntity Entity);
        void SetDetached(TEntity Entity);
        Task Add(TEntity entity, bool isSaveChanges = true);

        Task Change(TEntity entity, bool isSaveChanges = true);

        Task Delete<T>(T id, bool isSaveChanges = true);
        Task DeleteRange<T>(List<T> ids, bool isSaveChanges = true);
        Task RemoveRangeAsync(List<TEntity> entities, bool isSaveChanges = true);
        Task RemoveAsync(TEntity entity, bool isSaveChanges = true);
        Task AddRangeAsync(List<TEntity> entities, bool isSaveChanges = true);
        public Task ChangeRangeAsync(List<TEntity> entities, bool isSaveChanges = true);
        Task Save();
        Task SoftDelete(BaseEntity entity);

    }
}
