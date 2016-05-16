// ----------------------------------------------------------------------------
// <copyright file="CustomerService.svc.cs" company="SOGETI Spain">
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
    /// Represents the customer Web Service.
    /// </summary>
    public sealed class CustomerService : ICustomerService
    {
        #region Fields

        /// <summary>
        /// Defines the customer repository.
        /// </summary>
        private readonly ICustomerRepository customerRepository;

        /// <summary>
        /// Defines the mapper.
        /// </summary>
        private readonly IMapper mapper;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerService"/> class.
        /// </summary>
        public CustomerService()
        {
            this.customerRepository = ServiceLocator.Current.GetInstance<ICustomerRepository>();
            this.mapper = ServiceLocator.Current.GetInstance<IMapper>();
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Gets all customer.
        /// </summary>
        /// <returns>
        /// The all retrieved customer.
        /// </returns>
        public async Task<IEnumerable<CustomerDto>> GetAllAsync()
        {
            return await Task.Factory.StartNew(() =>
            {
                IEnumerable<Customer> customers = this.customerRepository.GetAll();
                IEnumerable<CustomerDto> customerDtos = customers.Select(
                    customer => this.mapper.Map<Customer, CustomerDto>(customer)).ToList();

                return customerDtos;
            });
        }

        /// <summary>
        /// Gets a customer by its identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// The retrieved customer.
        /// </returns>
        public async Task<CustomerDto> GetByIdAsync(int id)
        {
            return await Task.Factory.StartNew(() =>
            {
                Customer customer = this.customerRepository.GetById(id);
                CustomerDto customerDto = this.mapper.Map<Customer, CustomerDto>(customer);

                return customerDto;
            });
        }

        #endregion Methods
    }
}