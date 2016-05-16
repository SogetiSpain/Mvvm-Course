// ----------------------------------------------------------------------------
// <copyright file="CustomerEditViewModel.cs" company="SOGETI Spain">
//     Copyright © 2016 SOGETI Spain. All rights reserved.
//     MVVM Course by Óscar Fernández González a.k.a. Osc@rNET.
// </copyright>
// ----------------------------------------------------------------------------
namespace SogetiSpain.MvvmCourse.UI.Customers
{
    using System;
    using Services;
    using WebServices;

    /// <summary>
    /// Represents the view model for customer edit view.
    /// </summary>
    public sealed class CustomerEditViewModel : BindableBase
    {
        #region Fields

        /// <summary>
        /// Defines the customer service client.
        /// </summary>
        private readonly ICustomerServiceClient customerServiceClient;

        /// <summary>
        /// Defines the customer.
        /// </summary>
        private CustomerItem customer;

        /// <summary>
        /// Defines the editing customer.
        /// </summary>
        private CustomerDto editingCustomer;

        /// <summary>
        /// Defines the value indicating whether the view is in edit mode.
        /// </summary>
        private bool editMode;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerEditViewModel" /> class.
        /// </summary>
        /// <param name="customerServiceClient">The customer service client.</param>
        public CustomerEditViewModel(ICustomerServiceClient customerServiceClient)
        {
            this.customerServiceClient = customerServiceClient;
            this.CancelCommand = new RelayCommand(this.OnCancel);
            this.SaveCommand = new RelayCommand(this.OnSave, this.CanSave);
        }

        #endregion Constructors

        #region Events

        /// <summary>
        /// Notifies clients that data editing was done.
        /// </summary>
        public event Action Done = delegate { };

        #endregion Events

        #region Properties

        /// <summary>
        /// Gets the cancel command.
        /// </summary>
        /// <value>
        /// The cancel command.
        /// </value>
        public RelayCommand CancelCommand
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the customer.
        /// </summary>
        /// <value>
        /// The customer.
        /// </value>
        public CustomerItem Customer
        {
            get
            {
                return this.customer;
            }

            set
            {
                this.SetProperty(ref this.customer, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the view is in edit mode.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the view is in edit mode; otherwise, <c>false</c>.
        /// </value>
        public bool EditMode
        {
            get
            {
                return this.editMode;
            }

            set
            {
                this.SetProperty(ref this.editMode, value);
            }
        }

        /// <summary>
        /// Gets the save command.
        /// </summary>
        /// <value>
        /// The save command.
        /// </value>
        public RelayCommand SaveCommand
        {
            get;
            private set;
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Sets the customer.
        /// </summary>
        /// <param name="customer">The customer.</param>
        public void SetCustomer(CustomerDto customer)
        {
            this.editingCustomer = customer;

            if (this.Customer != null)
            {
                this.Customer.ErrorsChanged -= this.RaiseCanExecuteChanged;
            }

            this.Customer = new CustomerItem();
            this.Customer.ErrorsChanged += this.RaiseCanExecuteChanged;
            this.CopyCustomer(this.editingCustomer, this.Customer);
        }

        /// <summary>
        /// Determines whether the user can save the data.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if the user can save the data; otherwise, <c>false</c>.
        /// </returns>
        private bool CanSave()
        {
            return !this.Customer.HasErrors;
        }

        /// <summary>
        /// Copies the customer.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="target">The target.</param>
        private void CopyCustomer(CustomerDto source, CustomerItem target)
        {
            target.Address = source.Address;
            target.City = source.City;
            target.Company = source.Company;
            target.Country = source.Country;
            target.Email = source.Email;
            target.Fax = source.Fax;
            target.FirstName = source.FirstName;
            target.Id = source.Id;
            target.LastName = source.LastName;
            target.Phone = source.Phone;
            target.PostalCode = source.PostalCode;
            target.State = source.State;
        }

        /// <summary>
        /// Called when the user cancel data editing.
        /// </summary>
        private void OnCancel()
        {
            this.Done();
        }

        /// <summary>
        /// Called when the user save the data.
        /// </summary>
        private async void OnSave()
        {
            this.UpdateCustomer(this.Customer, this.editingCustomer);

            if (this.EditMode)
            {
                await this.customerServiceClient.Proxy.UpdateAsync(this.editingCustomer);
            }
            else
            {
                int customerId = await this.customerServiceClient.Proxy.CreateAsync(this.editingCustomer);
                this.editingCustomer.Id = customerId;
                this.Customer.Id = customerId;
            }

            this.Done();
        }

        /// <summary>
        /// Raises "CanExecuteChanged" on the UI thread so every command invoker can request to check if the command can execute.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void RaiseCanExecuteChanged(object sender, EventArgs e)
        {
            this.SaveCommand.RaiseCanExecuteChanged();
        }

        /// <summary>
        /// Updates the customer.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="target">The target.</param>
        private void UpdateCustomer(CustomerItem source, CustomerDto target)
        {
            target.Address = source.Address;
            target.City = source.City;
            target.Company = source.Company;
            target.Country = source.Country;
            target.Email = source.Email;
            target.Fax = source.Fax;
            target.FirstName = source.FirstName;
            target.Id = source.Id;
            target.LastName = source.LastName;
            target.Phone = source.Phone;
            target.PostalCode = source.PostalCode;
            target.State = source.State;
        }

        #endregion Methods
    }
}