namespace PublicApi.DTO.v1.Mapper
{
    public class DTOMapper
    {
        public CartMealDTOCreate MapCartMealDTO(BLL.App.DTO.CartMeal BLLOwner)
        {
            
        }
        public CartMealDTOEdit MapCartMealDTO(BLL.App.DTO.CartMeal BLLOwner)
        {
            
        }
        public CartMealDTO MapCartMealDTO(BLL.App.DTO.CartMeal BLLOwner)
        {
            return new CartMealDTO()
            {
                Id = BLLOwner.Id,
                
            };
        }
    }
}