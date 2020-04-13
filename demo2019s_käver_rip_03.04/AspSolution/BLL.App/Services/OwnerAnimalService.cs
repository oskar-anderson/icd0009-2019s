using BLL.Base.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.BLL.Base.Mappers;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using Contracts.DAL.Base.Repositories;
using DAL.App.EF;
using Domain;

namespace BLL.App.Services
{
    public class OwnerAnimalService : BaseEntityService<IOwnerAnimalRepository, IAppUnitOfWork, OwnerAnimal, OwnerAnimal>, IOwnerAnimalService
    {
        public OwnerAnimalService(IAppUnitOfWork unitOfWork) 
            : base(unitOfWork, new IdentityMapper<OwnerAnimal, OwnerAnimal>(), unitOfWork.OwnerAnimals)
        {
        }
    }
}