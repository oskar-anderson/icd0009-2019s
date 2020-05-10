﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BLL.App.Mappers;
using BLL.Base.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;


namespace BLL.App.Services
{
    public class CategoryService :
        BaseEntityService<IAppUnitOfWork, ICategoryRepository, ICategoryServiceMapper, DAL.App.DTO.Category,
        BLL.App.DTO.Category>, ICategoryService
    {
        public CategoryService(IAppUnitOfWork uow) : 
            base(uow, uow.Categorys, new CategoryServiceMapper())
        {
        }
    }
}