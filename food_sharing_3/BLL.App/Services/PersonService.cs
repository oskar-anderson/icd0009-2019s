using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.DTO;
using BLL.App.Mappers;
using BLL.Base.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;

namespace BLL.App.Services
{
    public class PersonService :
        BaseEntityService<IAppUnitOfWork, IPersonRepository, IPersonServiceMapper, DAL.App.DTO.Person,
            BLL.App.DTO.Person>, IPersonService
    {
        public PersonService(IAppUnitOfWork uow) : 
            base(uow, uow.Persons, new PersonServiceMapper())
        {
        }

        public virtual async Task<IEnumerable<Person>> GetAllForViewAsync()
        {
            return (await Repository.GetAllForViewAsync()).Select(e => Mapper.MapPersonView(e));
        }
    }
}