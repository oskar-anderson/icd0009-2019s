using System;

namespace PublicApi.DTO.v1.Mappers
{
    public class DTOMapper
    {
        /*
        public OwnerCreate MapOwnerCreate(BLL.App.DTO.Owner BLLOwner)
        {
            
        }
        
        public OwnerEdit MapOwnerEdit(BLL.App.DTO.Owner BLLOwner)
        {
            
        }
        */
        
        public Owner MapOwner(BLL.App.DTO.Owner BLLOwner)
        {
            return new Owner()
            {
                Id = BLLOwner.Id,
                FirstName = BLLOwner.FirstName,
                LastName = BLLOwner.LastName,
                AnimalCount = BLLOwner.AnimalCount,
            }; 
        }
        
        
    }
}