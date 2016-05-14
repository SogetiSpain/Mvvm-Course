// ----------------------------------------------------------------------------
// <copyright file="IEmployeeService.cs" company="SOGETI Spain">
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
    /// Defines the contract for the employee Web Service.
    /// </summary>
    [ServiceContract]
    public interface IEmployeeService
    {
        #region Methods

        /// <summary>
        /// Gets all employees.
        /// </summary>
        /// <returns>
        /// The all retrieved employees.
        /// </returns>
        [OperationContract]
        Task<IEnumerable<EmployeeDto>> GetAllAsync();

        /// <summary>
        /// Gets an employee by its identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// The retrieved employee.
        /// </returns>
        [OperationContract]
        Task<EmployeeDto> GetByIdAsync(int id);

        /// <summary>
        /// Updates a given employee.
        /// </summary>
        /// <param name="employeeDto">The employee data transfer object.</param>
        /// <returns>
        ///   <c>true</c> if the specified employee was updated sucessfully; otherwise, <c>false</c>.
        /// </returns>
        [OperationContract]
        Task<bool> UpdateAsync(EmployeeDto employeeDto);

        #endregion Methods
    }
}