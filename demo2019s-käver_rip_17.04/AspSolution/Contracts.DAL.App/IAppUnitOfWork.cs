using Contracts.DAL.App.Repositories;
using Contracts.DAL.Base;

namespace Contracts.DAL.App
{
    public interface IAppUnitOfWork : IBaseUnitOfWork
    {
        IAnimalRepository Animals { get;  }
        IOwnerRepository Owners { get;  }
        IOwnerAnimalRepository OwnerAnimals { get;  }
    }
}