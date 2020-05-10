﻿using System;
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
    public class InvoiceService : 
        BaseEntityService<IAppUnitOfWork, IInvoiceRepository, IInvoiceServiceMapper, DAL.App.DTO.Invoice,
            BLL.App.DTO.Invoice>, IInvoiceService
    {
        public InvoiceService(IAppUnitOfWork uow) : 
            base(uow, uow.Invoices, new InvoiceServiceMapper())
        {
        }


        public virtual async Task<IEnumerable<Invoice>> GetAllForViewAsync()
        {
            return (await Repository.GetAllForViewAsync()).Select(e => Mapper.MapInvoiceView(e));
        }
    }
}