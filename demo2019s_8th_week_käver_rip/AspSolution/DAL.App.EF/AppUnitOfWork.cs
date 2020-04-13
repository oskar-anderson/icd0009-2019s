using System;
using System.Collections.Generic;
using System.ComponentModel;
using Contracts.DAL.App;
using Contracts.DAL.Base;
using Contracts.DAL.Base.Repositories;
using DAL.Base.EF;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF
{
    public class AppUnitOfWork : EFBaseUnitOfWork<AppDbContext>, IAppUnitOfWork
    {
        public AppUnitOfWork(AppDbContext uowDbContext) : base(uowDbContext)
        {
        }

        /*
        public IAuthorRepository Authors =>
            GetRepository<IAuthorRepository>(() => new AuthorRepository(UOWDbContext));

        public IBaseRepository<Book> Books =>
            GetRepository<IBaseRepository<Book>>(() => new EFBaseRepository<Book, AppDbContext>(UOWDbContext));
        */
        
    }
}