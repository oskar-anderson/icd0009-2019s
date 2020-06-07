using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ee.itcollege.kaande.pitsariina.DAL.Base.EF
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