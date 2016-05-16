// ----------------------------------------------------------------------------
// <copyright file="CustomerEditViewModel.cs" company="SOGETI Spain">
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
    /// Represents the view model for customer edit view.
    /// </summary>
    public sealed class CustomerEditViewModel : BindableBase
    {
        #region Fields

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
        /// Initializes a new instance of the <see cref="CustomerEditViewModel"/> class.
        /// </summary>
        public CustomerEditViewModel()
        {
        }

        #endregion Constructors

        #region Properties

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

        #endregion Properties

        #region Methods

        /// <summary>
        /// Sets the customer.
        /// </summary>
        /// <param name="customer">The customer.</param>
        public void SetCustomer(CustomerDto customer)
        {
            this.editingCustomer = customer;
        }

        #endregion Methods
    }
}