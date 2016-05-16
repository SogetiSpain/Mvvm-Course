// ----------------------------------------------------------------------------
// <copyright file="ServiceClient{TService}.cs" company="SOGETI Spain">
//     Copyright © 2016 SOGETI Spain. All rights reserved.
//     MVVM Course by Óscar Fernández González a.k.a. Osc@rNET.
// </copyright>
// ----------------------------------------------------------------------------
namespace SogetiSpain.MvvmCourse.UI
{
    using System.ServiceModel;

    /// <summary>
    /// Represents an abstract service client.
    /// </summary>
    /// <typeparam name="TService">The type of the service.</typeparam>
    public abstract class ServiceClient<TService> : ClientBase<TService>, IServiceClient<TService>
        where TService : class
    {
        #region Fields

        /// <summary>
        /// Defines the value indicating whether this instance is disposed.
        /// </summary>
        private bool isDisposed = false;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceClient{TService}"/> class.
        /// </summary>
        public ServiceClient()
            : base(typeof(TService).FullName)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceClient{TService}"/> class.
        /// </summary>
        /// <param name="endpointConfigurationName">Name of the endpoint configuration.</param>
        public ServiceClient(string endpointConfigurationName)
            : base(endpointConfigurationName)
        {
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the proxy.
        /// </summary>
        /// <value>
        /// The proxy.
        /// </value>
        public TService Proxy
        {
            get
            {
                return this.Channel;
            }
        }

        #endregion Properties

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.isDisposed)
            {
                if (disposing)
                {
                    if (this.State == CommunicationState.Faulted)
                    {
                        this.Abort();
                    }
                    else
                    {
                        try
                        {
                            this.Close();
                        }
                        catch
                        {
                            this.Abort();
                        }
                    }

                    this.isDisposed = true;
                }
            }
        }
    }
}