using AutoMapper;
using BLL.Base.Mappers;
using Contracts.BLL.App.Mappers;
using DAL.App.DTO;
using BLLAppDTO=BLL.App.DTO;

namespace BLL.App.Mappers
{
    public class InvoiceLineServiceMapper : BaseMapper<InvoiceLine, BLLAppDTO.InvoiceLine>, IInvoiceLineServiceMapper
    {
        public InvoiceLineServiceMapper():base()
        {
            MapperConfigurationExpression.CreateMap<InvoiceLine, BLLAppDTO.InvoiceLine>();
            MapperConfigurationExpression.CreateMap<Cart, BLLAppDTO.Cart>();
            MapperConfigurationExpression.CreateMap<Invoice, BLLAppDTO.Invoice>();
            // add more mappings

            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
        public BLLAppDTO.InvoiceLine MapInvoiceLineView(InvoiceLine inObject)
        {
            return Mapper.Map<BLLAppDTO.InvoiceLine>(inObject);
        }
    }
}