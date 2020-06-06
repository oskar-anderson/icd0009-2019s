using System;
using System.Collections.Generic;
using System.ComponentModel;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using Contracts.DAL.Base;
using Contracts.DAL.Base.Repositories;
using DAL.App.EF.Repositories;
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

        public IAnimalRepository Animals =>
            GetRepository<IAnimalRepository>(() => new AnimalRepository(UOWDbContext));
        
        public IOwnerRepository Owners =>
            GetRepository<IOwnerRepository>(() => new OwnerRepository(UOWDbContext));
        public IOwnerAnimalRepository OwnerAnimals =>
            GetRepository<IOwnerAnimalRepository>(() => new OwnerAnimalRepository(UOWDbContext));
    }
}