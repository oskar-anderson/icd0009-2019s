using Contracts.BLL.App.Mappers;
using DAL.App.DTO;
using ee.itcollege.kaande.pitsariina.BLL.Base.Mappers;
using BLLAppDTO=BLL.App.DTO;

namespace BLL.App.Mappers
{
    public class CategoryServiceMapper : BaseMapper<Category, BLLAppDTO.Category>, ICategoryServiceMapper
    {
        
    }
}