﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IComponentPriceRepository : IBaseRepository<ComponentPrice>
    {
        Task<IEnumerable<ComponentPrice>> AllAsync(Guid? userId = null);
        Task<ComponentPrice> FirstOrDefaultAsync(Guid id, Guid? userId = null);

        Task<bool> ExistsAsync(Guid id, Guid? userId = null);
        Task DeleteAsync(Guid id, Guid? userId = null);
        
        // DTO methods
        // Task<IEnumerable<ComponentPriceDTO>> DTOAllAsync(Guid? userId = null);
        // Task<ComponentPriceDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null);
    }
}