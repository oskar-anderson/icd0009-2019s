using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.Base.Mappers;
using Contracts.BLL.Base.Services;
using Contracts.DAL.Base;
using Contracts.DAL.Base.Repositories;

namespace BLL.Base.Services
{
    public class BaseEntityService<TServiceRepository, TUnitOfWork, TDALEntity, TBLLEntity> : BaseService,
        IBaseEntityService<TBLLEntity>
        where TBLLEntity : class, IDomainEntity<Guid>, new()
        where TDALEntity : class, IDomainEntity<Guid>, new()
        where TUnitOfWork : IBaseUnitOfWork
        where TServiceRepository : IBaseRepository<TDALEntity>
    {
        protected readonly TUnitOfWork ServiceUnitOfWork;
        private readonly IBaseBLLMapper<TDALEntity, TBLLEntity> _mapper;
        protected readonly TServiceRepository ServiceRepository;

        public BaseEntityService(TUnitOfWork unitOfWork, IBaseBLLMapper<TDALEntity, TBLLEntity> mapper,
            TServiceRepository serviceRepository)
        {
            ServiceUnitOfWork = unitOfWork;
            ServiceRepository = serviceRepository;
            _mapper = mapper;

            // TODO - NOT POSSIBLE - we have no idea of what DAL actually is.
            // we have now BaseRepository implementation - cant call new on it
            // or asc for func methodToCreateRepo to create the correct repo
            //ServiceRepository = ServiceUnitOfWork.GetRepository<IBaseRepository<TDALEntity>>(methodToCreateRepo);
        }


        public virtual IEnumerable<TBLLEntity> All() =>
            ServiceRepository.All().Select(entity => _mapper.Map<TDALEntity, TBLLEntity>(entity));

        public virtual async Task<IEnumerable<TBLLEntity>> AllAsync() =>
            (await ServiceRepository.AllAsync()).Select(entity => _mapper.Map<TDALEntity, TBLLEntity>(entity));

        public virtual TBLLEntity Find(params object[] id) =>
            _mapper.Map<TDALEntity, TBLLEntity>(ServiceRepository.Find(id));

        public virtual async Task<TBLLEntity> FindAsync(params object[] id) =>
            _mapper.Map<TDALEntity, TBLLEntity>(await ServiceRepository.FindAsync(id));

        public virtual TBLLEntity Add(TBLLEntity entity) =>
            _mapper.Map<TDALEntity, TBLLEntity>(ServiceRepository.Add(_mapper.Map<TBLLEntity, TDALEntity>(entity)));

        public virtual TBLLEntity Update(TBLLEntity entity) =>
            _mapper.Map<TDALEntity, TBLLEntity>(ServiceRepository.Update(_mapper.Map<TBLLEntity, TDALEntity>(entity)));


        public virtual TBLLEntity Remove(TBLLEntity entity) =>
            _mapper.Map<TDALEntity, TBLLEntity>(ServiceRepository.Remove(_mapper.Map<TBLLEntity, TDALEntity>(entity)));


        public virtual TBLLEntity Remove(params object[] id) =>
            _mapper.Map<TDALEntity, TBLLEntity>(ServiceRepository.Remove(id));
    }
}