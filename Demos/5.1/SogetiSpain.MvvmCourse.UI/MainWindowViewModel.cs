// ----------------------------------------------------------------------------
// <copyright file="MainWindowViewModel.cs" company="SOGETI Spain">
//     Copyright © 2016 SOGETI Spain. All rights reserved.
//     MVVM Course by Óscar Fernández González a.k.a. Osc@rNET.
// </copyright>
// ----------------------------------------------------------------------------
namespace SogetiSpain.MvvmCourse.UI
{
    using Customers;
    using Microsoft.Practices.ServiceLocation;
    using OrderPreparation;
    using Orders;
    using WebServices;

    /// <summary>
    /// Represents the view model for main window.
    /// </summary>
    public sealed class MainWindowViewModel : BindableBase
    {
        #region Fields

        /// <summary>
        /// Defines the customer edit view model.
        /// </summary>
        private readonly CustomerEditViewModel customerEditViewModel;

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
            this.customerEditViewModel = ServiceLocator.Current.GetInstance<CustomerEditViewModel>();
            this.customerEditViewModel.Done += this.NavivationToCustomers;

            this.customerListViewModel = ServiceLocator.Current.GetInstance<CustomerListViewModel>();
            this.customerListViewModel.AddCustomerRequested += this.NavigateToAddCustomer;
            this.customerListViewModel.EditCustomerRequested += this.NavigateToEditCustomer;
            this.customerListViewModel.PlaceOrderRequested += this.NavigateToOrder;

            this.orderPreparationViewModel = new OrderPreparationViewModel();
            this.orderViewModel = new OrderViewModel();

            this.NavigateCommand = new RelayCommand<NavigationDestination>(this.OnNavigate);
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

        /// <summary>
        /// Gets the navigate command.
        /// </summary>
        /// <value>
        /// The navigate command.
        /// </value>
        public RelayCommand<NavigationDestination> NavigateCommand
        {
            get;
            private set;
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Navigates to add customer.
        /// </summary>
        /// <param name="customer">The customer.</param>
        private void NavigateToAddCustomer(CustomerDto customer)
        {
            this.customerEditViewModel.EditMode = false;
            this.customerEditViewModel.SetCustomer(customer);

            this.CurrentViewModel = this.customerEditViewModel;
        }

        /// <summary>
        /// Navigates to customer list.
        /// </summary>
        private void NavivationToCustomers()
        {
            this.CurrentViewModel = this.customerListViewModel;
        }

        /// <summary>
        /// Navigates to edit customer.
        /// </summary>
        /// <param name="customer">The customer.</param>
        private void NavigateToEditCustomer(CustomerDto customer)
        {
            this.customerEditViewModel.EditMode = true;
            this.customerEditViewModel.SetCustomer(customer);

            this.CurrentViewModel = this.customerEditViewModel;
        }

        /// <summary>
        /// Navigates to order.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        private void NavigateToOrder(int customerId)
        {
            this.orderViewModel.CustomerId = customerId;
            this.CurrentViewModel = this.orderViewModel;
        }

        /// <summary>
        /// Called when navigate.
        /// </summary>
        /// <param name="destination">The destination.</param>
        private void OnNavigate(NavigationDestination destination)
        {
            switch (destination)
            {
                case NavigationDestination.OrderPreparation:
                    this.CurrentViewModel = this.orderPreparationViewModel;
                    break;

                case NavigationDestination.Customers:
                default:
                    this.CurrentViewModel = this.customerListViewModel;
                    break;
            }
        }

        #endregion Methods
    }
}