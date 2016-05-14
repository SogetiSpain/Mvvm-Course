// ----------------------------------------------------------------------------
// <copyright file="Bootstrapper.cs" company="SOGETI Spain">
//     Copyright © 2016 SOGETI Spain. All rights reserved.
//     MVVM Course by Óscar Fernández González a.k.a. Osc@rNET.
// </copyright>
// ----------------------------------------------------------------------------
namespace SogetiSpain.MvvmCourse.WebServices
{
    using System;
    using System.Configuration;
    using System.Reflection;
    using AutoMapper;
    using Data;
    using Domain;
    using FluentNHibernate.Cfg;
    using FluentNHibernate.Cfg.Db;
    using Microsoft.Practices.ServiceLocation;
    using Microsoft.Practices.Unity;
    using Microsoft.Practices.Unity.InterceptionExtension;
    using NHibernate;
    using NHibernate.Dialect;
    using NHibernate.Driver;

    /// <summary>
    /// Provides a basic bootstrapping sequence of this application.
    /// </summary>
    internal static class Bootstrapper
    {
        #region Fields

        /// <summary>
        /// Defines the dependency injection container.
        /// </summary>
        private static readonly IUnityContainer Container = new UnityContainer();

        #endregion Fields

        #region Methods

        /// <summary>
        /// Runs this instance.
        /// </summary>
        public static void Run()
        {
            Bootstrapper.ConfigureInterceptors();
            Bootstrapper.SetLocatorProvider();

            Bootstrapper.RegisterDataContextFactory<ISessionFactory>(
                () => Bootstrapper.CreateNHibernateSessionFactory());

            Bootstrapper.RegisterMappings();
        }

        /// <summary>
        /// Configures the interceptors.
        /// </summary>
        private static void ConfigureInterceptors()
        {
            Interceptor<InterfaceInterceptor> interfaceInterceptor =
                new Interceptor<InterfaceInterceptor>();

            InterceptionBehavior<UnitOfWorkInterceptionBehavior> unitOfWorkInterceptionBehavior =
                new InterceptionBehavior<UnitOfWorkInterceptionBehavior>();

            Bootstrapper.Container
                .AddNewExtension<Interception>()
                .RegisterType<IArtistRepository, ArtistRepository>(
                    interfaceInterceptor,
                    unitOfWorkInterceptionBehavior);

            Bootstrapper.Container
                .AddNewExtension<Interception>()
                .RegisterType<IEmployeeRepository, EmployeeRepository>(
                    interfaceInterceptor,
                    unitOfWorkInterceptionBehavior);
            /*
                .RegisterType<IPersonService, PersonService>(
                    interfaceInterceptor,
                    unitOfWorkInterceptionBehavior);
            */
        }

        /// <summary>
        /// Creates a new NHibernate session factory.
        /// </summary>
        /// <returns>
        /// The created NHibernate session factory.
        /// </returns>
        private static ISessionFactory CreateNHibernateSessionFactory()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MvvmCourseDB"].ConnectionString;

            return Fluently.Configure()
                           .Database(
                                OracleDataClientConfiguration.Oracle10
                                .ConnectionString(connectionString)
                                .Dialect<Oracle10gDialect>()
                                .Driver<OracleManagedDataClientDriver>())
                           .Mappings(e => e.FluentMappings.AddFromAssembly(Assembly.GetAssembly(typeof(ArtistMap))))
                           .BuildSessionFactory();
        }

        /// <summary>
        /// Registers the specified data context factory.
        /// </summary>
        /// <typeparam name="TDataContext">The type of the data context.</typeparam>
        /// <param name="createDataContextFactory">The function for create the data context factory.</param>
        private static void RegisterDataContextFactory<TDataContext>(Func<TDataContext> createDataContextFactory)
        {
            Bootstrapper.Container.RegisterType<TDataContext>(
                new ContainerControlledLifetimeManager(),
                new InjectionFactory(e => createDataContextFactory()));
        }

        /// <summary>
        /// Registers the mappings.
        /// </summary>
        private static void RegisterMappings()
        {
            MapperConfiguration mapperCfg = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Artist, ArtistDto>();

                cfg.CreateMap<Employee, EmployeeDto>()
                .ForMember(dto => dto.Address, map => map.MapFrom(entity => entity.Address.AddressLine1))
                .ForMember(dto => dto.City, map => map.MapFrom(entity => entity.Address.City))
                .ForMember(dto => dto.Country, map => map.MapFrom(entity => entity.Address.Country))
                .ForMember(dto => dto.PostalCode, map => map.MapFrom(entity => entity.Address.PostalCode))
                .ForMember(dto => dto.State, map => map.MapFrom(entity => entity.Address.State))
                .ReverseMap()
                .ForSourceMember(dto => dto.Address, map => map.Ignore())
                .ForSourceMember(dto => dto.City, map => map.Ignore())
                .ForSourceMember(dto => dto.Country, map => map.Ignore())
                .ForSourceMember(dto => dto.PostalCode, map => map.Ignore())
                .ForSourceMember(dto => dto.State, map => map.Ignore())
                .ForMember(
                    entity => entity.Address,
                    map => map.ResolveUsing(
                        dto =>
                        {
                            return new Address(
                                dto.Address,
                                dto.City,
                                dto.State,
                                dto.Country,
                                dto.PostalCode);
                        }));
            });

            mapperCfg.AssertConfigurationIsValid();

            Bootstrapper.Container.RegisterInstance(
                mapperCfg.CreateMapper(),
                new ContainerControlledLifetimeManager());
        }

        /// <summary>
        /// Sets the locator provider.
        /// </summary>
        private static void SetLocatorProvider()
        {
            IServiceLocator unityServiceLocator = new UnityServiceLocator(Bootstrapper.Container);
            ServiceLocator.SetLocatorProvider(() => unityServiceLocator);
        }

        #endregion Methods
    }
}