using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.Base.Mappers;
using Contracts.BLL.Base.Services;
using Contracts.DAL.Base;
using Contracts.DAL.Base.Repositories;

namespace BLL.Base.Service
{
    public class BaseEntityService<TRepository, TUOW, TDALEntity, TBLLEntity> : BaseService, 
        IBaseEntityService<TBLLEntity>
        where TBLLEntity : class, IDomainBaseEntity<Guid>, new()
        where TDALEntity : class, IDomainBaseEntity<Guid>, new()
        where TUOW : IBaseUnitOfWork
        where TRepository : IBaseRepository<TDALEntity>
    {

        protected readonly TUOW UOW;
        protected readonly TRepository ServiceRepository;
        protected readonly IBaseBLLMapper<TDALEntity, TBLLEntity> Mapper;
        
        public BaseEntityService(TUOW uow, IBaseBLLMapper<TDALEntity, TBLLEntity> mapper, TRepository serviceRepository)
        {
            UOW = uow;
            ServiceRepository = serviceRepository;
            Mapper = mapper;
            
            // TODO - NOT POSSIBLE - we have no idea of what DAL actually is.
            // we have now BaseRepository implementation -cant call new on it
            // or asc for func methodToCreateRepo to create the correct repo
            // ServiceRepository = ServiceUnitOfWork.GetRepository(() => new BaseRe);
        }
        
        public virtual IEnumerable<TBLLEntity> All()
        {
            return ServiceRepository.All().Select(e => Mapper.Map<TDALEntity, TBLLEntity>(e));
        }

        public virtual async Task<IEnumerable<TBLLEntity>> AllAsync()
        {
            return (await ServiceRepository.AllAsync()).Select(e => Mapper.Map<TDALEntity, TBLLEntity>(e));
        }

        public virtual TBLLEntity Find(params object[] id)
        {
            return Mapper.Map<TDALEntity, TBLLEntity>(ServiceRepository.Find(id));
        }

        public virtual async Task<TBLLEntity> FindAsync(params object[] id)
        {
            return Mapper.Map<TDALEntity, TBLLEntity>(await ServiceRepository.FindAsync(id));
        }

        public virtual TBLLEntity Add(TBLLEntity entity)
        {
            return Mapper.Map<TDALEntity, TBLLEntity>(ServiceRepository.Add(Mapper.Map<TBLLEntity, TDALEntity>(entity)));
        }

        public virtual TBLLEntity Update(TBLLEntity entity)
        {
            return Mapper.Map<TDALEntity, TBLLEntity>(ServiceRepository.Update(Mapper.Map<TBLLEntity, TDALEntity>(entity)));
        }

        public virtual TBLLEntity Remove(TBLLEntity entity)
        {
            return Mapper.Map<TDALEntity, TBLLEntity>(ServiceRepository.Remove(Mapper.Map<TBLLEntity, TDALEntity>(entity)));
        }

        public virtual TBLLEntity Remove(params object[] id)
        {
            return Mapper.Map<TDALEntity, TBLLEntity>(ServiceRepository.Remove(id));
        }
    }
}