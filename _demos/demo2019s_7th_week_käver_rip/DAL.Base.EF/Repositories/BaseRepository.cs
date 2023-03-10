using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.Base;
using Contracts.DAL.Base.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DAL.Base.EF.Repositories
{
    public class BaseRepository<TEntity> : BaseRepository<TEntity, Guid>
        where TEntity : class, IDomainEntity<Guid>, new()
    {
        public BaseRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }

    public class BaseRepository<TEntity, TKey> : IBaseRepository<TEntity, TKey>
        where TEntity : class, IDomainEntity<TKey>, new()
        where TKey : struct, IComparable
    {
        protected DbContext RepoDbContext;
        protected DbSet<TEntity> RepoDbSet;
        public BaseRepository(DbContext dbContext)
        {
            RepoDbContext = dbContext;
            RepoDbSet = RepoDbContext.Set<TEntity>();
            if (RepoDbSet == null)
            {
               throw new ArgumentNullException(typeof(TEntity).Name + " was not found as DBSet!");
            }
        }
        
        public virtual IEnumerable<TEntity> All()
        {
            return RepoDbSet.ToList();
        }

        public virtual async Task<IEnumerable<TEntity>> AllAsync()
        {
            return await RepoDbSet.ToListAsync();
        }

        public virtual TEntity Find(params object[] id)
        {
            return RepoDbSet.Find(id);
        }

        public virtual async Task<TEntity> FindAsync(params object[] id)
        {
            return await RepoDbSet.FindAsync(id);
        }

        public virtual TEntity Add(TEntity entity)
        {
            return RepoDbSet.Add(entity).Entity;
        }

        public virtual TEntity Update(TEntity entity)
        {
            return RepoDbSet.Update(entity).Entity;
        }

        public virtual TEntity Remove(TEntity entity)
        {
            return RepoDbSet.Remove(entity).Entity;
        }

        public virtual TEntity Remove(params object[] id)
        {
            return Remove(Find(id));
        }

        public virtual int SaveChanges()
        {
            return RepoDbContext.SaveChanges();
        }

        public virtual async Task<int> SaveChangesAsync()
        {
            return await RepoDbContext.SaveChangesAsync();
        }
    }
    
}