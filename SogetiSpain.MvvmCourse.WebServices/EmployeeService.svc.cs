// ----------------------------------------------------------------------------
// <copyright file="EmployeeService.svc.cs" company="SOGETI Spain">
//     Copyright © 2016 SOGETI Spain. All rights reserved.
//     MVVM Course by Óscar Fernández González a.k.a. Osc@rNET.
// </copyright>
// ----------------------------------------------------------------------------
namespace SogetiSpain.MvvmCourse.WebServices
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using Domain;
    using Microsoft.Practices.ServiceLocation;

    /// <summary>
    /// Represents the employee Web Service.
    /// </summary>
    public sealed class EmployeeService : IEmployeeService
    {
        #region Fields

        /// <summary>
        /// Defines the employee repository.
        /// </summary>
        private readonly IEmployeeRepository employeeRepository;

        /// <summary>
        /// Defines the mapper.
        /// </summary>
        private readonly IMapper mapper;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeeService"/> class.
        /// </summary>
        public EmployeeService()
        {
            this.employeeRepository = ServiceLocator.Current.GetInstance<IEmployeeRepository>();
            this.mapper = ServiceLocator.Current.GetInstance<IMapper>();
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Gets all employees.
        /// </summary>
        /// <returns>
        /// The all retrieved employees.
        /// </returns>
        public async Task<IEnumerable<EmployeeDto>> GetAllAsync()
        {
            return await Task.Factory.StartNew(() =>
            {
                IEnumerable<Employee> employees = this.employeeRepository.GetAll();
                IEnumerable<EmployeeDto> employeeDtos = employees.Select(
                    employee => this.mapper.Map<Employee, EmployeeDto>(employee)).ToList();

                return employeeDtos;
            });
        }

        /// <summary>
        /// Gets an employee by its identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// The retrieved employee.
        /// </returns>
        public async Task<EmployeeDto> GetByIdAsync(int id)
        {
            return await Task.Factory.StartNew(() =>
            {
                Employee employee = this.employeeRepository.GetById(id);
                EmployeeDto employeeDto = this.mapper.Map<Employee, EmployeeDto>(employee);

                return employeeDto;
            });
        }

        /// <summary>
        /// Updates a given employee.
        /// </summary>
        /// <param name="employeeDto">The employee data transfer object.</param>
        /// <returns>
        ///   <c>true</c> if the specified employee was updated successfully; otherwise, <c>false</c>.
        /// </returns>
        public async Task<bool> UpdateAsync(EmployeeDto employeeDto)
        {
            return await Task.Factory.StartNew(() =>
            {
                Employee employee = this.employeeRepository.GetById(employeeDto.Id);
                if (employee != null)
                {
                    employee = this.mapper.Map(employeeDto, employee);
                    this.employeeRepository.Modify(employee);

                    return true;
                }

                return false;
            });
        }

        #endregion Methods
    }
}