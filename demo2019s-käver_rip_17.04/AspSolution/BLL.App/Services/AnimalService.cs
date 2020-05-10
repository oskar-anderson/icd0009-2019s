using BLL.App.DTO;
using BLL.Base.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;

namespace BLL.App.Services
{
    public class AnimalService : BaseEntityService<IAnimalRepository, IAppUnitOfWork, DAL.App.DTO.Animal, Animal>, IAnimalService
    {
        public AnimalService(IAppUnitOfWork unitOfWork) 
            : base(unitOfWork, new BaseBLLMapper<DAL.App.DTO.Animal, Animal>(), unitOfWork.Animals)
        {
        }
    }
}