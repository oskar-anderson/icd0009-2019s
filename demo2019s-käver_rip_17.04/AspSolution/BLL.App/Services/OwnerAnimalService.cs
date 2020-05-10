using BLL.App.DTO;
using BLL.Base.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;

namespace BLL.App.Services
{
    public class OwnerAnimalService : BaseEntityService<IOwnerAnimalRepository, IAppUnitOfWork, DAL.App.DTO.OwnerAnimal, OwnerAnimal>, IOwnerAnimalService
    {
        public OwnerAnimalService(IAppUnitOfWork unitOfWork) 
            : base(unitOfWork, new BaseBLLMapper<DAL.App.DTO.OwnerAnimal, OwnerAnimal>(), unitOfWork.OwnerAnimals)
        {
        }
    }
}