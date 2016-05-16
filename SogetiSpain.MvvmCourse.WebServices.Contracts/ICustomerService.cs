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
        /// Creates a specified customer.
        /// </summary>
        /// <param name="customerDto">The customer data transfer object.</param>
        /// <returns>
        /// The customer identifier.
        /// </returns>
        [OperationContract]
        Task<int> CreateAsync(CustomerDto customerDto);

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

        /// <summary>
        /// Updates a given customer.
        /// </summary>
        /// <param name="customerDto">The customer data transfer object.</param>
        /// <returns>
        ///   <c>true</c> if the specified customer was updated successfully; otherwise, <c>false</c>.
        /// </returns>
        [OperationContract]
        Task<bool> UpdateAsync(CustomerDto customerDto);

        #endregion Methods
    }
}