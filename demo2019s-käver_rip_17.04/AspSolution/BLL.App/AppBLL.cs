using System;
using System.Threading.Tasks;
using BLL.App.Services;
using BLL.Base;
using Contracts.BLL.App;
using Contracts.BLL.App.Services;
using Contracts.BLL.Base;
using Contracts.DAL.App;
using DAL.App.EF;

namespace BLL.App
{
    public class AppBLL : BaseBLL<IAppUnitOfWork>, IAppBLL
    {
        public AppBLL(IAppUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public IAnimalService Animals =>
            GetService<IAnimalService>(() => new AnimalService(UnitOfWork));

        public IOwnerService Owners =>
            GetService<IOwnerService>(() => new OwnerService(UnitOfWork));

        public IOwnerAnimalService OwnerAnimals =>
            GetService<IOwnerAnimalService>(() => new OwnerAnimalService(UnitOfWork));
    }
}