using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1
{
    
    public class OwnerCreate
    {
        [MinLength(1)] [MaxLength(64)] 
        public string FirstName { get; set; } = default!;
        [MinLength(1)] [MaxLength(64)] 
        public string LastName { get; set; } = default!;
    }
    
    public class OwnerEdit: OwnerCreate
    {
        public Guid Id { get; set; }
    }
    
    public class Owner : OwnerEdit
    {
        public int AnimalCount { get; set; }        
    }
    

    

}