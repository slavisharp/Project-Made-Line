namespace MadeLine.Data.Repository
{
    using System;
    using System.Linq;

    using Models;
    using Microsoft.EntityFrameworkCore;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    public class Repository<T> : IRepository<T>
        where T : class
    {
        public Repository(ApplicationDbContext context)
        {
            this.Context = context ?? throw new ArgumentException("An instance of DbContext is required to use this repository.", nameof(context));
            this.DbSet = this.Context.Set<T>();
        }

        private DbSet<T> DbSet { get; }

        public DbContext Context { get; }
        
        /// <summary>
        /// Returns all entities from the DB
        /// </summary>
        public IQueryable<T> All()
        {
            return this.DbSet;
        }
        
        public T GetById(object id)
        {
            return this.DbSet.Find(id);
        }

        public Task<T> GetByIdAsync(object id)
        {
            return this.DbSet.FindAsync(id);
        }

        //public Task<IQueryable<T>> GetRange(IEnumerable<object> ids)
        //{
        //    return this.DbSet.Where(e => ids.Contains(e.Id));
        //}

        public void Add(T entity)
        {
            this.DbSet.Add(entity);
        }

        public Task AddAsync(T entity)
        {
            return this.DbSet.AddAsync(entity);
        }

        public void AddRange(IEnumerable<T> entities)
        {
            this.DbSet.AddRange(entities);
        }

        public Task AddRangeAsync(IEnumerable<T> entities)
        {
            return this.DbSet.AddRangeAsync(entities);
        }

        /// <summary>
        /// Marks entity as deleted
        /// </summary>
        public void Delete(T entity)
        {
            if (entity is IDeletableEntity)
            {
                var deletable = entity as IDeletableEntity;
                deletable.IsDeleted = true;
                deletable.DeletedOn = DateTime.UtcNow;
            }
            else
            {
                this.DbSet.Remove(entity);
            }
        }

        /// <summary>
        /// Removes entity from the DB
        /// </summary>
        public void HardDelete(T entity)
        {
            this.DbSet.Remove(entity);
        }

        /// <summary>
        /// Save entities changes to the DB
        /// </summary>
        public void Save()
        {
            this.Context.SaveChanges();
        }

        public Task SaveAsync()
        {
            return this.Context.SaveChangesAsync();
        }
    }
}
