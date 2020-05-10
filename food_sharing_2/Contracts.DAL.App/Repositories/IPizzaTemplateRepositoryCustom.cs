﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Base.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IPizzaTemplateRepositoryCustom: IPizzaTemplateRepositoryCustom<PizzaTemplate>
    {
    }

    public interface IPizzaTemplateRepositoryCustom<TPizzaTemplate>
    {
        Task<IEnumerable<TPizzaTemplate>> GetAllAsyncSpecific_DALAppEF_BLLBase(Guid id, Guid? userId = null, bool noTracking = true);
    }

    
}