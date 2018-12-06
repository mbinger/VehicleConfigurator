using DAL.Common.Equipment;
using DAL.Common.Interface;
using Microsoft.Extensions.DependencyInjection;
using DAL.Repository;
using DAL.Common.Booking;

namespace Service.Start
{
    /// <summary>
    /// Configure repositories
    /// </summary>
    public class RepositoryConfig
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IRepository<AdditionalEquipmentItem>, AdditionalEquipmentRepository>();
            services.AddTransient<IRepository<Color>, ColorRepository>();
            services.AddTransient<IRepository<Engine>, EngineRepository>();
            services.AddTransient<IRepository<Rim>, RimRepository>();
            services.AddTransient<IRepository<Car>, CarRepository>();
            services.AddTransient<IRepository<Order>, OrderRepository>();
            services.AddTransient<IRepository<OrderAdditionalEquipmentItem>, OrderEquipmentRepository>();
        }
    }
}
