// ----------------------------------------------------------------------------
// <copyright file="ICustomerService.cs" company="SOGETI Spain">
//     Copyright © 2016 SOGETI Spain. All rights reserved.
//     MVVM Course by Óscar Fernández González a.k.a. Osc@rNET.
// </copyright>
// ----------------------------------------------------------------------------
namespace SogetiSpain.MvvmCourse.WebServices
{
    using System.Collections.Generic;
    using System.ServiceModel;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the contract for the customer Web Service.
    /// </summary>
    [ServiceContract]
    public interface ICustomerService
    {
        #region Methods

        /// <summary>
        /// Gets all customer.
        /// </summary>
        /// <returns>
        /// The all retrieved customer.
        /// </returns>
        [OperationContract]
        Task<IEnumerable<CustomerDto>> GetAllAsync();

        /// <summary>
        /// Gets a customer by its identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// The retrieved customer.
        /// </returns>
        [OperationContract]
        Task<CustomerDto> GetByIdAsync(int id);

        #endregion Methods
    }
}