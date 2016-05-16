// ----------------------------------------------------------------------------
// <copyright file="CustomerListViewModel.cs" company="SOGETI Spain">
//     Copyright © 2016 SOGETI Spain. All rights reserved.
//     MVVM Course by Óscar Fernández González a.k.a. Osc@rNET.
// </copyright>
// ----------------------------------------------------------------------------
namespace SogetiSpain.MvvmCourse.UI.Customers
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using WebServices;
    using WebServices.Client.CustomerServiceClient;

    /// <summary>
    /// Represents the view model for customer list view.
    /// </summary>
    public sealed class CustomerListViewModel : BindableBase
    {
        #region Fields

        /// <summary>
        /// Defines the customer collection.
        /// </summary>
        private ObservableCollection<CustomerDto> customers;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerListViewModel"/> class.
        /// </summary>
        public CustomerListViewModel()
        {
            this.AddCustomerCommand = new RelayCommand(this.OnAddCustomer);
            this.EditCustomerCommand = new RelayCommand<CustomerDto>(this.OnEditCustomer);
            this.PlaceOrderCommand = new RelayCommand<CustomerDto>(this.OnPlaceOrder);
        }

        #endregion Constructors

        #region Events

        /// <summary>
        /// Notifies clients that add a customer is requested.
        /// </summary>
        public event Action<CustomerDto> AddCustomerRequested = delegate { };

        /// <summary>
        /// Notifies clients that edit a customer is requested.
        /// </summary>
        public event Action<CustomerDto> EditCustomerRequested = delegate { };

        /// <summary>
        /// Notifies clients that place an order is requested.
        /// </summary>
        public event Action<int> PlaceOrderRequested = delegate { };

        #endregion Events

        #region Properties

        /// <summary>
        /// Gets the add customer command.
        /// </summary>
        /// <value>
        /// The add customer command.
        /// </value>
        public RelayCommand AddCustomerCommand
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the customers.
        /// </summary>
        /// <value>
        /// The customers.
        /// </value>
        public ObservableCollection<CustomerDto> Customers
        {
            get
            {
                return this.customers;
            }

            set
            {
                this.SetProperty(ref this.customers, value);
            }
        }

        /// <summary>
        /// Gets the edit customer command.
        /// </summary>
        /// <value>
        /// The edit customer command.
        /// </value>
        public RelayCommand<CustomerDto> EditCustomerCommand
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the place order command.
        /// </summary>
        /// <value>
        /// The place order command.
        /// </value>
        public RelayCommand<CustomerDto> PlaceOrderCommand
        {
            get;
            private set;
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Loads the customers.
        /// </summary>
        public async void LoadCustomers()
        {
            this.Customers = null;
            using (CustomerServiceClient serviceClient = new CustomerServiceClient())
            {
                IEnumerable<CustomerDto> customerDtos = await serviceClient.GetAllAsync();
                this.Customers = new ObservableCollection<CustomerDto>(customerDtos);
            }
        }

        /// <summary>
        /// Called when the user add a customer.
        /// </summary>
        private void OnAddCustomer()
        {
            this.AddCustomerRequested(new CustomerDto());
        }

        /// <summary>
        /// Called when the user edit a customer.
        /// </summary>
        /// <param name="customer">The customer.</param>
        private void OnEditCustomer(CustomerDto customer)
        {
            this.EditCustomerRequested(customer);
        }

        /// <summary>
        /// Called when the user place an order.
        /// </summary>
        /// <param name="customer">The customer.</param>
        private void OnPlaceOrder(CustomerDto customer)
        {
            this.PlaceOrderRequested(customer.Id);
        }

        #endregion Methods
    }
}