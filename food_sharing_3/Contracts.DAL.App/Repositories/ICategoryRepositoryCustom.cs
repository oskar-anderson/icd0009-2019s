using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface ICategoryRepositoryCustom: ICategoryRepositoryCustom<Category>
    {
    }

    public interface ICategoryRepositoryCustom<TCategory>
    {
        
    }

    
}