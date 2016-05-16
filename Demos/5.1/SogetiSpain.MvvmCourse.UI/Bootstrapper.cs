// ----------------------------------------------------------------------------
// <copyright file="Bootstrapper.cs" company="SOGETI Spain">
//     Copyright © 2016 SOGETI Spain. All rights reserved.
//     MVVM Course by Óscar Fernández González a.k.a. Osc@rNET.
// </copyright>
// ----------------------------------------------------------------------------
namespace SogetiSpain.MvvmCourse.UI
{
    using Microsoft.Practices.ServiceLocation;
    using Microsoft.Practices.Unity;
    using Services;

    /// <summary>
    /// Provides a basic bootstrapping sequence of this UI application.
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
            Bootstrapper.RegisterTypes();
            Bootstrapper.SetLocatorProvider();
        }

        /// <summary>
        /// Registers the types.
        /// </summary>
        private static void RegisterTypes()
        {
            Bootstrapper.Container.RegisterType<ICustomerServiceClient, CustomerServiceClient>(
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