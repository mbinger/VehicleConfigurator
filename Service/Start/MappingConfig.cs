using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using DAL.Common.Booking;
using DAL.Common.Equipment;
using DAL.Common.Interface;
using DAL.Common.Reference;
using Microsoft.Extensions.DependencyInjection;
using Service.Common.DataTransfer;
using Service.Common.Interface;

namespace Service
{
    /// <summary>
    /// Configure mappings
    /// </summary>
    public class MappingConfig
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfileConfiguration());
            });

            var mapper = config.CreateMapper();
            services.AddSingleton(mapper);
        }

        private class AutoMapperProfileConfiguration : Profile
        {
            public AutoMapperProfileConfiguration()
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

            private void Map<TEntity, TDto>()
           where TEntity : IEntity
           where TDto : IDataTransferObject
            {
                CreateMap<TEntity, TDto>();
                CreateMap<TDto, TEntity>();
            }
        }
    }
}