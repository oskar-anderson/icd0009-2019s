using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.Mappers;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
using ee.itcollege.kaande.pitsariina.BLL.Base.Services;


namespace BLL.App.Services
{
    public class ComponentService : 
        BaseEntityService<IAppUnitOfWork, IComponentRepository, IComponentServiceMapper, Component,
            BLL.App.DTO.Component>, IComponentService
    {
        public ComponentService(IAppUnitOfWork uow) : 
            base(uow, uow.Components, new ComponentServiceMapper())
        {
        }

    }
}