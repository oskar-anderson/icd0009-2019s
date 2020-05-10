using AutoMapper;
using BLL.Base.Mappers;
using Contracts.BLL.App.Mappers;
using DAL.App.DTO;
using BLLAppDTO=BLL.App.DTO;

namespace BLL.App.Mappers
{
    public class InvoiceServiceMapper : BaseMapper<Invoice, BLLAppDTO.Invoice>, IInvoiceServiceMapper
    {
        public InvoiceServiceMapper():base()
        {
            MapperConfigurationExpression.CreateMap<Invoice, BLLAppDTO.Invoice>();
            MapperConfigurationExpression.CreateMap<Restaurant, BLLAppDTO.Restaurant>();
            MapperConfigurationExpression.CreateMap<Person, BLLAppDTO.Person>();
            MapperConfigurationExpression.CreateMap<PaymentMethod, BLLAppDTO.PaymentMethod>();
            // add more mappings

            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
        public BLLAppDTO.Invoice MapInvoiceView(Invoice inObject)
        {
            return Mapper.Map<BLLAppDTO.Invoice>(inObject);
        }
    }
}