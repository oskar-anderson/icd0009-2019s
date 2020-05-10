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
    public class InvoiceLineService : 
        BaseEntityService<IAppUnitOfWork, IInvoiceLineRepository, IInvoiceLineServiceMapper, DAL.App.DTO.InvoiceLine,
            BLL.App.DTO.InvoiceLine>, IInvoiceLineService
    {
        public InvoiceLineService(IAppUnitOfWork uow) : 
            base(uow, uow.InvoiceLines, new InvoiceLineServiceMapper())
        {
        }

        public virtual async Task<IEnumerable<InvoiceLine>> GetAllForViewAsync()
        {
            return (await Repository.GetAllForViewAsync()).Select(e => Mapper.MapInvoiceLineView(e));
        }
    }
}