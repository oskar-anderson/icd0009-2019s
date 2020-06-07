using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.App.DTO;
using ee.itcollege.kaande.pitsariina.Contracts.DAL.Base.Repositories;

namespace Contracts.DAL.App.Repositories
{
    public interface IComponentPizzaTemplateRepository : IBaseRepository<ComponentPizzaTemplate>, IComponentPizzaTemplateRepositoryCustom
    {
    }
}