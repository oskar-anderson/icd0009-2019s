﻿using AutoMapper;
using BLL.Base.Mappers;
using Contracts.BLL.App.Mappers;
using DAL.App.DTO;
using BLLAppDTO=BLL.App.DTO;

namespace BLL.App.Mappers
{
    public class SharingItemServiceMapper : BaseMapper<SharingItem, BLLAppDTO.SharingItem>, ISharingItemServiceMapper
    {
        public SharingItemServiceMapper():base()
        {
            MapperConfigurationExpression.CreateMap<SharingItem, BLLAppDTO.SharingItem>();
            MapperConfigurationExpression.CreateMap<Sharing, BLLAppDTO.Sharing>();
            MapperConfigurationExpression.CreateMap<Item, BLLAppDTO.Item>();
            // add more mappings

            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
        public BLLAppDTO.SharingItem MapSharingItemView(SharingItem inObject)
        {
            return Mapper.Map<BLLAppDTO.SharingItem>(inObject);
        }
    }
}