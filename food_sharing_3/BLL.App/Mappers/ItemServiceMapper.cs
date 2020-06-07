﻿using AutoMapper;
using Contracts.BLL.App.Mappers;
using DAL.App.DTO;
using DAL.App.DTO.Identity;
using ee.itcollege.kaande.pitsariina.BLL.Base.Mappers;
using BLLAppDTO=BLL.App.DTO;

namespace BLL.App.Mappers
{
    public class ItemServiceMapper : BaseMapper<Item, BLLAppDTO.Item>, IItemServiceMapper
    {
        public ItemServiceMapper():base()
        {
            MapperConfigurationExpression.CreateMap<Item, BLLAppDTO.Item>();
            MapperConfigurationExpression.CreateMap<Sharing, BLLAppDTO.Sharing>();
            MapperConfigurationExpression.CreateMap<AppUser, BLLAppDTO.Identity.AppUser>();
            // add more mappings

            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
        public BLLAppDTO.Item MapItemView(Item inObject)
        {
            return Mapper.Map<BLLAppDTO.Item>(inObject);
        }
    }
}