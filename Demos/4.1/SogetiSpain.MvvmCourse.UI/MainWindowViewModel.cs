// ----------------------------------------------------------------------------
// <copyright file="MainWindowViewModel.cs" company="SOGETI Spain">
//     Copyright © 2016 SOGETI Spain. All rights reserved.
//     MVVM Course by Óscar Fernández González a.k.a. Osc@rNET.
// </copyright>
// ----------------------------------------------------------------------------
namespace SogetiSpain.MvvmCourse.UI
{
    using Customers;
    using OrderPreparation;
    using Orders;

    /// <summary>
    /// Represents the view model for main window.
    /// </summary>
    public sealed class MainWindowViewModel : BindableBase
    {
        #region Fields

        /// <summary>
        /// Defines the customer list view model.
        /// </summary>
        private readonly CustomerListViewModel customerListViewModel;

        /// <summary>
        /// Defines the order preparation view model.
        /// </summary>
        private readonly OrderPreparationViewModel orderPreparationViewModel;

        /// <summary>
        /// Defines the order view model.
        /// </summary>
        private readonly OrderViewModel orderViewModel;

        /// <summary>
        /// Defines the current view model.
        /// </summary>
        private BindableBase currentViewModel;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindowViewModel"/> class.
        /// </summary>
        public MainWindowViewModel()
        {
            this.customerListViewModel = new CustomerListViewModel();
            this.orderPreparationViewModel = new OrderPreparationViewModel();
            this.orderViewModel = new OrderViewModel();
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets the current view model.
        /// </summary>
        /// <value>
        /// The current view model.
        /// </value>
        public BindableBase CurrentViewModel
        {
            get
            {
                return this.currentViewModel;
            }

            set
            {
                this.SetProperty(ref this.currentViewModel, value);
            }
        }

        #endregion Properties
    }
}