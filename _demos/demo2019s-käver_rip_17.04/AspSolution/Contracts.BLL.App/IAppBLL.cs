using System;
using Contracts.BLL.App.Services;
using Contracts.BLL.Base;

namespace Contracts.BLL.App
{
    public interface IAppBLL : IBaseBLL
    {
        public IAnimalService Animals { get; }
        public IOwnerService Owners { get; }
        public IOwnerAnimalService OwnerAnimals { get; }
    }
}