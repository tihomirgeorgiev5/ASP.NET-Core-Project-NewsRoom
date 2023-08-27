using Microsoft.EntityFrameworkCore;
using NewsRoom.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NewsRoom.Infrastructure.Data.Common
{
    public abstract class Repository : IRepository
    {
        protected DbContext dbContext;

        public Repository(NewsRoomDbContext context)
        {
            this.dbContext = context;
        }

        private DbSet<T> DbSet<T>()
            where T : class
        {
            return this.dbContext.Set<T>();
        }

        public void Add<T>(T entity)
            where T : class
        {
            this.DbSet<T>().Add(entity);
        }

        public async Task AddAsync<T>(T entity)
            where T : class
        {
            await this.DbSet<T>().AddAsync(entity);
        }

        public IQueryable<T> All<T>() 
            where T : class
        {
            return this.DbSet<T>().
                AsQueryable();
        }

        public IQueryable<T> AllReadonly<T>()
            where T : class
        {
            return this.DbSet<T>()
                .AsQueryable()
                .AsNoTracking();
        }

        public IQueryable<T> AllWithDeleted<T>()
            where T : class 
        {
            return this.DbSet<T>().AsQueryable().IgnoreQueryFilters();
        }  
        

        public void Delete<T>(T entity)
            where T : class
        {
            this.dbContext.Set<T>()
                .Remove(entity);
        }

        public async Task DeleteAsync<T>(object id)
            where T : class
        {
            T entity = await this.GetByIdAsync<T>(id);

            Delete<T>(entity);
        }

        public async Task<T> GetByIdAsync<T>(object id)
            where T : class
        {
            return await DbSet<T>().FirstAsync((CancellationToken)id);
        }

        public void HardDelete<T>(T entity)
            where T : class
        {
            this.DbSet<T>().Remove(entity);
        }

        public int SaveChanges()
        {
            return this.dbContext.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await this.dbContext.SaveChangesAsync();
        }

        public void Update<T>(T entity)
            where T : class
        {
            this.DbSet<T>().Update(entity);
        }

        public void UpdateRange<T>(IEnumerable<T> entities)
            where T : class
        {
            this.DbSet<T>().UpdateRange(entities);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.dbContext?.Dispose();
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~Repository()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
