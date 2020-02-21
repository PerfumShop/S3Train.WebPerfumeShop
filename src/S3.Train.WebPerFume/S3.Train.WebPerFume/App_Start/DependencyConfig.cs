﻿using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using S3.Train.WebPerFume.Models;
using S3Train.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.UI;

namespace S3.Train.WebPerFume.App_Start
{
    public class DependencyConfig
    {
        public static IContainer RegisterDependencyResolvers()
        {
            ContainerBuilder builder = new ContainerBuilder();
            RegisterDependencyMappingDefaults(builder);
            RegisterDependencyMappingOverrides(builder);
            IContainer container = builder.Build();
            // Set Up MVC Dependency Resolver
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            // Set Up WebAPI Resolver
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            return container;
        }

        private static void RegisterDependencyMappingDefaults(ContainerBuilder builder)
        {
            Assembly coreAssembly = Assembly.GetAssembly(typeof(IStateManager));
            Assembly webAssembly = Assembly.GetAssembly(typeof(MvcApplication));

            builder.RegisterAssemblyTypes(coreAssembly).AsImplementedInterfaces().InstancePerRequest();
            builder.RegisterAssemblyTypes(webAssembly).AsImplementedInterfaces().InstancePerRequest();

            builder.RegisterControllers(webAssembly);
            builder.RegisterModule(new AutofacWebTypesModule());
        }

        private static void RegisterDependencyMappingOverrides(ContainerBuilder builder)
        {
            builder.RegisterType<ApplicationDbContext>();
            builder.RegisterType<BannerService>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<BrandService>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<CategoryService>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<OrderService>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<ProductImageService>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<ProductService>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<ProductAdvertisementService>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<ShoppingCartService>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<ShoppingCartDetailService>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<VendorService>().AsImplementedInterfaces().SingleInstance();

        }
    }
}