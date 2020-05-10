﻿using AutoMapper;
using BLL.Base.Mappers;
using Contracts.BLL.App.Mappers;
using DAL.App.DTO;
using DAL.App.DTO.Identity;
using BLLAppDTO=BLL.App.DTO;

namespace BLL.App.Mappers
{
    public class CartServiceMapper : BaseMapper<Cart, BLLAppDTO.Cart>, ICartServiceMapper
    {
        public CartServiceMapper():base()
        {
            MapperConfigurationExpression.CreateMap<Cart, BLLAppDTO.Cart>();
            MapperConfigurationExpression.CreateMap<Restaurant, BLLAppDTO.Restaurant>();
            MapperConfigurationExpression.CreateMap<AppUser, BLLAppDTO.Identity.AppUser>();
            // add more mappings

            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
        
        public BLLAppDTO.Cart MapCartView(Cart inObject)
        {
            return Mapper.Map<BLLAppDTO.Cart>(inObject);
        }
    }
}