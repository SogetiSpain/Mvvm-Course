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
        /// Creates a specified customer.
        /// </summary>
        /// <param name="customerDto">The customer data transfer object.</param>
        /// <returns>
        /// The customer identifier.
        /// </returns>
        public async Task<int> CreateAsync(CustomerDto customerDto)
        {
            return await Task.Factory.StartNew(() =>
            {
                Customer customer = this.mapper.Map(customerDto, new Customer());
                this.customerRepository.Add(customer);

                return customer.Id;
            });
        }

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

        /// <summary>
        /// Updates a given customer.
        /// </summary>
        /// <param name="customerDto">The customer data transfer object.</param>
        /// <returns>
        /// <c>true</c> if the specified customer was updated successfully; otherwise, <c>false</c>.
        /// </returns>
        public async Task<bool> UpdateAsync(CustomerDto customerDto)
        {
            return await Task.Factory.StartNew(() =>
            {
                Customer customer = this.customerRepository.GetById(customerDto.Id);
                if (customer != null)
                {
                    customer = this.mapper.Map(customerDto, customer);
                    this.customerRepository.Modify(customer);

                    return true;
                }

                return false;
            });
        }

        #endregion Methods
    }
}