using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base;
using Contracts.DAL.Base.Repositories;
using DAL.Base;
using Microsoft.EntityFrameworkCore;

namespace Domain.Base.EF
{
    public class EFBaseUnitOfWork<TKey, TDbContext> : BaseUnitOfWork<TKey>
        where TDbContext : DbContext 
        where TKey : IEquatable<TKey>

    {
        protected readonly TDbContext UOWDbContext;
        

        public EFBaseUnitOfWork(TDbContext uowDbContext)
        {
            UOWDbContext = uowDbContext;
        }

        public override async Task<int> SaveChangesAsync()
        {
            // calls DAL.App.EF/AppDbContext/SaveChangesAsynch which generates metadata
            var result =  await UOWDbContext.SaveChangesAsync();
            
            UpdateTrackedEntities();
             
            return result;
        }
    }
}