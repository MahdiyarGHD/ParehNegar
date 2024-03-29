using Microsoft.EntityFrameworkCore;
using ParehNegar.Domain;
using ParehNegar.Domain.Interfaces;
using ParehNegar.Logics.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ParehNegar.Logics.DatabaseLogics
{
    public class GenericQueryBuilder<TEntity, TId> where TEntity : class, IIdSchema<TId> where TId : new()
    {
        private readonly DbContext _context;

        public GenericQueryBuilder(DbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();

            if (filter != null)
                query = query.Where(filter);

            return await query.ToListAsync();
        }

        public async Task<TEntity> GetByAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await _context.Set<TEntity>().FirstOrDefaultAsync(filter);
        }

        public async Task<TEntity> GetByIdAsync(object id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            if (entity is IDateTimeSchema dateTimeSchema)
            {
                dateTimeSchema.CreationDateTime = DateTime.Now;
                dateTimeSchema.ModificationDateTime = null;
            }

            if (entity is ISoftDeleteSchema softDeleteSchema)
            {
                softDeleteSchema.IsDeleted = false;
                softDeleteSchema.DeletedDateTime = null;
            }

            _context.Set<TEntity>().Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task AddBulkAsync(IEnumerable<TEntity> entities)
        {
            await _context.Set<TEntity>().AddRangeAsync(entities);
            await _context.SaveChangesAsync();
        }

        public async Task<TEntity> UpdateAsync(TEntity entity, bool allowSchemaUpdate = true)
        {
            var entry = _context.Entry(entity);
            entry.State = EntityState.Modified;

            if (entity is IDateTimeSchema dateTimeSchema && allowSchemaUpdate)
                dateTimeSchema.ModificationDateTime = DateTime.Now;

            if (typeof(TEntity).GetProperty("CreationDateTime") is not null)
                entry.Property("CreationDateTime").IsModified = false;
            
            if (typeof(TEntity).GetProperty("IsDeleted") is not null && allowSchemaUpdate)
                entry.Property("IsDeleted").IsModified = false;
            
            if (typeof(TEntity).GetProperty("DeletedDateTime") is not null && allowSchemaUpdate)
                entry.Property("DeletedDateTime").IsModified = false;

            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity> UpdateChangedValuesOnlyAsync(TEntity newEntity)
        {
            TEntity existingEntity = await GetByIdAsync(newEntity.Id) ?? throw new KeyNotFoundException("Entity not found");
            var updatedEntity = existingEntity;
            var properties = typeof(TEntity).GetProperties();

            foreach (var property in properties)
            {
                if (property.CanWrite)
                {
                    var newValue = property.GetValue(newEntity);
                    if(newValue == null)
                        property.SetValue(updatedEntity, property.GetValue(existingEntity));
                    else if (!Equals(property.GetValue(existingEntity), newValue))
                        property.SetValue(updatedEntity, newValue);
                }
            }

            await UpdateAsync(updatedEntity);

            return updatedEntity;
        }

        public async Task UpdateBulkAsync(IEnumerable<TEntity> entities, bool allowSchemaUpdate = true)
        {
            foreach (var entity in entities)
            {
                var entry = _context.Entry(entity);
                entry.State = EntityState.Modified;

                if (entity is IDateTimeSchema dateTimeSchema && allowSchemaUpdate)
                    dateTimeSchema.ModificationDateTime = DateTime.Now;

                if (typeof(TEntity).GetProperty("CreationDateTime") is not null)
                    entry.Property("CreationDateTime").IsModified = false;

                if (typeof(TEntity).GetProperty("IsDeleted") is not null && allowSchemaUpdate)
                    entry.Property("IsDeleted").IsModified = false;

                if (typeof(TEntity).GetProperty("DeletedDateTime") is not null && allowSchemaUpdate)
                    entry.Property("DeletedDateTime").IsModified = false;
            }

            await _context.SaveChangesAsync();
        }

        public async Task<TEntity> SoftDeleteByIdAsync(TId id)
        {
            TEntity entity = await GetByIdAsync(id);
            if (entity == null)
                return null;

            if (entity is ISoftDeleteSchema softDeleteSchema)
            {
                softDeleteSchema.IsDeleted = true;
                softDeleteSchema.DeletedDateTime = DateTime.Now;
                await UpdateAsync(entity, false);
                return entity;
            }
            else
            {
                throw new InvalidOperationException("Soft delete is not supported for this entity type.");
            }
        }

        public async Task<TEntity> HardDeleteByIdAsync(TId id)
        {
            TEntity entity = await GetByIdAsync(id);
            if (entity == null)
                return null;

            _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<int> BulkSoftDeleteByIdAsync(IEnumerable<TId> ids)
        {
            var entities = await _context.Set<TEntity>().Where(e => ids.Contains(e.Id)).ToListAsync();
            int deletedCount = 0;

            foreach (var entity in entities)
            {
                if (entity is ISoftDeleteSchema softDeleteSchema)
                {
                    softDeleteSchema.IsDeleted = true;
                    softDeleteSchema.DeletedDateTime = DateTime.Now;
                    deletedCount++;
                }
            }

            await UpdateBulkAsync(entities, false);
            return deletedCount;
        }

        public async Task<int> BulkHardDeleteByIdAsync(IEnumerable<TId> ids)
        {
            var entities = await _context.Set<TEntity>().Where(e => ids.Contains(e.Id)).ToListAsync();
            _context.Set<TEntity>().RemoveRange(entities);
            int deletedCount = await _context.SaveChangesAsync();
            return deletedCount;
        }
    }

}
