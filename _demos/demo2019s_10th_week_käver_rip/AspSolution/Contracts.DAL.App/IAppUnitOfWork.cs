using Contracts.DAL.App.Repositories;
using Contracts.DAL.Base;
using Contracts.DAL.Base.Repositories;
using Domain;

namespace Contracts.DAL.App
{
    public interface IAppUnitOfWork : IBaseUnitOfWork
    {
        IAnimalRepository Animals { get;  }
        IOwnerRepository Owners { get;  }
        IOwnerAnimalRepository OwnerAnimals { get;  }
    }
}