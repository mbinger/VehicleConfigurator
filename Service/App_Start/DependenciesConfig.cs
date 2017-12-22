using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using DAL.Common.Booking;
using DAL.Common.Equipment;
using DAL.Common.Interface;
using DAL.Context;
using DAL.Repository;
using DAL.Validation;
using Service.Autofac;

namespace Service
{
    public class DependenciesConfig
    {
        public static void RegisterDependencies()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<AutofacServiceLocator>().As<IServiceLocator>();

            builder.RegisterType<CarValidator>().Named<IEntityValidator>(typeof (Car).Name);
            builder.RegisterType<EngineValidator>().Named<IEntityValidator>(typeof (Engine).Name);
            builder.RegisterType<ColorValidator>().Named<IEntityValidator>(typeof (Color).Name);
            builder.RegisterType<RimValidator>().Named<IEntityValidator>(typeof (Rim).Name);
            builder.RegisterType<AdditionalEquipmentItemValidator>()
                .Named<IEntityValidator>(typeof (AdditionalEquipmentItem).Name);
            builder.RegisterType<OrderValidator>().Named<IEntityValidator>(typeof (Order).Name);

            builder.RegisterType<ConfigDbContext>().UsingConstructor(typeof (IServiceLocator));
            builder.RegisterType<AdditionalEquipmentRepository>().As<IRepository<AdditionalEquipmentItem>>();
            builder.RegisterType<ColorRepository>().As<IRepository<Color>>();
            builder.RegisterType<EngineRepository>().As<IRepository<Engine>>();
            builder.RegisterType<RimRepository>().As<IRepository<Rim>>();
            builder.RegisterType<CarRepository>().As<IRepository<Car>>();
            builder.RegisterType<OrderRepository>().As<IRepository<Order>>();
            builder.RegisterType<OrderEquipmentRepository>().As<IRepository<OrderAdditionalEquipmentItem>>();

            #region MVC & WebApi registration

            // Get your HttpConfiguration.
            var config = GlobalConfiguration.Configuration;

            // Register your Web API controllers.
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // OPTIONAL: Register the Autofac filter provider.
            builder.RegisterWebApiFilterProvider(config);

            // OPTIONAL: Register the Autofac model binder provider.
            builder.RegisterWebApiModelBinderProvider();

            //builder.RegisterControllers(Assembly.GetExecutingAssembly());

            var container = builder.Build();
            //var autofacDependencyResolver = new AutofacDependencyResolver(container);
            //DependencyResolver.SetResolver(autofacDependencyResolver);

            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            #endregion
        }
    }
}