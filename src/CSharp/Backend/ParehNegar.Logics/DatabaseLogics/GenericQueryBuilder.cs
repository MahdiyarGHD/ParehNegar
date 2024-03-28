using Microsoft.EntityFrameworkCore;
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
            _context.Set<TEntity>().Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task AddBulkAsync(IEnumerable<TEntity> entities)
        {
            await _context.Set<TEntity>().AddRangeAsync(entities);
            await _context.SaveChangesAsync();
        }

        //public async Task UpdateAsync(TEntity entity)
        //{
        //    _context.Entry(entity).State = EntityState.Modified;
        //    await _context.SaveChangesAsync();
        //}

        //public async Task UpdateChangedValuesOnlyAsync(TEntity entity)
        //{
        //    _context.Set<TEntity>().Add(entity);

        //    var entry = _context.Entry(entity);
        //    foreach (var propertyName in entry.OriginalValues.Properties)
        //    {
        //        if (!EqualityComparer<object>.Default.Equals(entry.OriginalValues[propertyName], entry.CurrentValues[propertyName]))
        //        {
        //            entry.Property(propertyName).IsModified = true;
        //        }
        //    }

        //    await _context.SaveChangesAsync();
        //}

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
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

        public async Task UpdateBulkAsync(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().UpdateRange(entities);
            await _context.SaveChangesAsync();
        }
    }

}
