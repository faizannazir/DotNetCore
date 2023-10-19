﻿using DataAccess.Entities.BaseEntity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataAccess.GenericRepository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {

        private readonly DbContext _dbContext;

        public Repository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual IQueryable<TEntity> GetWithCondition(Expression<Func<TEntity, bool>> expression)
        {
            return _dbContext.Set<TEntity>().Where(expression);
        }

        public IQueryable<TEntity> GetAll()
        {
            return _dbContext.Set<TEntity>();
        }
        public IQueryable<TEntity> GetReadOnlyList()
        {
            return _dbContext.Set<TEntity>().AsNoTracking();
        }
        public async Task<TEntity> Get<T>(T id)
        {
            return await _dbContext.Set<TEntity>().FindAsync(id);
        }

        public async Task Add(TEntity entity, bool isSaveChanges = true)
        {
            await _dbContext.Set<TEntity>().AddAsync(entity);
            if (isSaveChanges)
            {
                await Save();
            }
        }


        public async Task Change(TEntity entity, bool isSaveChanges = true)
        {
            _dbContext.Set<TEntity>().Update(entity);
            if (isSaveChanges)
            {
                await Save();
            }
        }

        public async Task Delete<T>(T id, bool isSaveChanges = true)
        {

            var entity = await Get(id);


            if (entity != null)
            {
                _dbContext.Set<TEntity>().Remove(entity);
                if (isSaveChanges)
                {
                    await Save();
                }
            }

        }
        public async Task DeleteRange<T>(List<T> ids, bool isSaveChanges = true)
        {
            foreach (var item in ids)
            {
                await Delete(item, isSaveChanges);
            }
        }
        public async Task Save()
        {
            await _dbContext.SaveChangesAsync();
        }



        public IQueryable<TEntity> GetNotDeleted()
        {
            object deleted = true;
            return GetAll().Where(x => typeof(TEntity).GetProperty("IsDeleted").GetValue(x) != deleted);
        }

        public async Task SoftDelete(BaseEntity entity)
        {
            entity.IsDeleted = true;
            entity.DeletedDate = DateTime.Now.ToUniversalTime();
            await Save();
        }

        public async Task RemoveRangeAsync(List<TEntity> entities, bool isSaveChanges = true)
        {
            _dbContext.RemoveRange(entities);
            if (isSaveChanges)
                await Save();


        }

        public async Task AddRangeAsync(List<TEntity> entities, bool isSaveChanges = true)
        {
            if (entities != null && entities.Count > 0)
            {
                await _dbContext.AddRangeAsync(entities);
                if (isSaveChanges)
                    await Save();
            }
        }

        public async Task ChangeRangeAsync(List<TEntity> entities, bool isSaveChanges = true)
        {
            if (entities != null && entities.Count > 0)
            {
                _dbContext.Set<TEntity>().UpdateRange(entities);
                if (isSaveChanges)
                    await Save();
            }
        }

        public void SetAdded(TEntity Entity)
        {
            this._dbContext.Entry(Entity).State = EntityState.Added;
        }

        public void SetDetached(TEntity Entity)
        {
            this._dbContext.Entry(Entity).State = EntityState.Detached;
        }

        public async Task RemoveAsync(TEntity entity, bool isSaveChanges = true)
        {
            _dbContext.Remove(entity);
            if (isSaveChanges)
                await Save();
        }

        private void SetUpdateAnalysisValue(TEntity entity, bool isSoftDelete)
        {
            var entityProperties = entity.GetType().GetProperties();
            var property = entityProperties.FirstOrDefault(x => x.Name.ToLower() == "updateddate");
            if (property != null)
                property.SetValue(entity, DateTime.UtcNow);

            if (isSoftDelete)
            {
                property = entityProperties.FirstOrDefault(x => x.Name.ToLower() == "isdeleted");
                if (property != null)
                    property.SetValue(entity, true);
            }
        }
        private void SetCreateAnalysisValue(TEntity entity)
        {
            var entityProperties = entity.GetType().GetProperties();
            var property = entityProperties.FirstOrDefault(x => x.Name.ToLower() == "createddate");
            if (property != null)
                property.SetValue(entity, DateTime.UtcNow);
        }
    }
}
