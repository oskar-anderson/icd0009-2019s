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
    public class AnimalService : BaseEntityService<IAnimalRepository, IAppUnitOfWork, Animal, Animal>, IAnimalService
    {
        public AnimalService(IAppUnitOfWork unitOfWork) 
            : base(unitOfWork, new IdentityMapper<Animal, Animal>(), unitOfWork.Animals)
        {
        }
    }
}