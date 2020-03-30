using System;
using System.Collections.Generic;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using Contracts.DAL.Base;
using DAL.App.EF.Repositories;
using DAL.Base.EF;

namespace DAL.App.EF
{
    public class AppUnitOfWork : EFBaseUnitOfWork<AppDbContext>, IAppUnitOfWork
    {
        public AppUnitOfWork(AppDbContext uowDbContext) : base(uowDbContext)
        {
        }
        

        public ICartRepository Carts => 
            GetRepository<ICartRepository>(() => new CartRepository(UOWDbContext));
        
        private ICartRepository? _carts = null;
        //public ICartRepository Carts => _carts ??= new CartRepository(UOWDbContext);    // transient
        //public ICartRepository Carts => _carts == null ? _carts = new CartRepository(UOWDbContext) : _carts;    // transient


    }
}