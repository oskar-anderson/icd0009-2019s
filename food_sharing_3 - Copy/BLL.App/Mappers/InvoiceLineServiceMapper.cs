using BLL.Base.Mappers;
using Contracts.BLL.App.Mappers;
using DAL.App.DTO;
using BLLAppDTO=BLL.App.DTO;

namespace BLL.App.Mappers
{
    public class InvoiceLineServiceMapper : BaseMapper<InvoiceLine, BLLAppDTO.InvoiceLine>, IInvoiceLineServiceMapper
    {
        
    }
}