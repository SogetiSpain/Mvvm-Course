// ----------------------------------------------------------------------------
// <copyright file="OrderViewModel.cs" company="SOGETI Spain">
//     Copyright © 2016 SOGETI Spain. All rights reserved.
//     MVVM Course by Óscar Fernández González a.k.a. Osc@rNET.
// </copyright>
// ----------------------------------------------------------------------------
namespace SogetiSpain.MvvmCourse.UI.Orders
{
    using System;

    /// <summary>
    /// Represents the view model for order view.
    /// </summary>
    public sealed class OrderViewModel : BindableBase
    {
        #region Fields

        /// <summary>
        /// Defines the customer identifier.
        /// </summary>
        private int customerId;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderViewModel"/> class.
        /// </summary>
        public OrderViewModel()
        {
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets the customer identifier.
        /// </summary>
        /// <value>
        /// The customer identifier.
        /// </value>
        public int CustomerId
        {
            get
            {
                return this.customerId;
            }

            set
            {
                this.SetProperty(ref this.customerId, value);
            }
        }

        #endregion Properties
    }
}