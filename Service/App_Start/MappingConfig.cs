using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using DAL.Common.Booking;
using DAL.Common.Equipment;
using DAL.Common.Interface;
using DAL.Common.Reference;
using Service.Common.DataTransfer;
using Service.Common.Interface;

namespace Service
{
   public class MappingConfig
   {
      public static void RegisterMapping()
      {
         //create mapping from entity to data-transafer objects
         Map<Color, ColorDto>();
         Map<Engine, EngineDto>();
         Map<Rim, RimDto>();
         Map<AdditionalEquipmentItem, AdditionalEquipmentItemDto>();
         Map<Car, CarDto>();
         Map<Order, OrderDto>();
         Map<FuelTypeRef, FuelTypeDto>();
         Map<RimTypeRef, RimTypeDto>();
         Map<ColorTypeRef, ColorTypeDto>();
      }

      private static void Map<TEntity, TDto>()
         where TEntity : IEntity
         where TDto : IDataTransferObject
      {
         Mapper.CreateMap<TEntity, TDto>();
         Mapper.CreateMap<TDto, TEntity>();
      }
   }
}