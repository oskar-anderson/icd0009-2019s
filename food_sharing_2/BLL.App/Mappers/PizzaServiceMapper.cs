﻿using BLL.Base.Mappers;
using Contracts.BLL.App.Mappers;
using BLLAppDTO=BLL.App.DTO;
using DALAppDTO=Domain.Base.App.DTO;
namespace BLL.App.Mappers
{
    public class PizzaServiceMapper : BaseMapper<DALAppDTO.Pizza, BLLAppDTO.Pizza>, IPizzaServiceMapper
    {
        
    }
}