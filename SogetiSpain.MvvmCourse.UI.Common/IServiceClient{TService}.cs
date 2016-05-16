// ----------------------------------------------------------------------------
// <copyright file="IServiceClient{TService}.cs" company="SOGETI Spain">
//     Copyright © 2016 SOGETI Spain. All rights reserved.
//     MVVM Course by Óscar Fernández González a.k.a. Osc@rNET.
// </copyright>
// ----------------------------------------------------------------------------
namespace SogetiSpain.MvvmCourse.UI
{
    using System;

    /// <summary>
    /// Defines the contract for a service client.
    /// </summary>
    /// <typeparam name="TService">The type of the service.</typeparam>
    public interface IServiceClient<TService> : IDisposable
        where TService : class
    {
        #region Properties

        /// <summary>
        /// Gets the proxy.
        /// </summary>
        /// <value>
        /// The proxy.
        /// </value>
        TService Proxy
        {
            get;
        }

        #endregion Properties
    }
}