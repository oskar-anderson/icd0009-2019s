using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.Mappers;
using BLL.Base.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;

namespace BLL.App.Services
{
    public class SizeService : 
        BaseEntityService<IAppUnitOfWork, ISizeRepository, ISizeServiceMapper, DAL.App.DTO.Size,
            BLL.App.DTO.Size>, ISizeService
    {
        public SizeService(IAppUnitOfWork uow) : 
            base(uow, uow.Sizes, new SizeServiceMapper())
        {
        }
        

    }
}