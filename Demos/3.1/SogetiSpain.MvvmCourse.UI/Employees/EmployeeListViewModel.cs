// ----------------------------------------------------------------------------
// <copyright file="EmployeeListViewModel.cs" company="SOGETI Spain">
//     Copyright © 2016 SOGETI Spain. All rights reserved.
//     MVVM Course by Óscar Fernández González a.k.a. Osc@rNET.
// </copyright>
// ----------------------------------------------------------------------------
namespace SogetiSpain.MvvmCourse.UI.Employees
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Windows;
    using WebServices;
    using WebServices.Client.EmployeeServiceClient;

    /// <summary>
    /// Represents the view model for Employee list view.
    /// </summary>
    public sealed class EmployeeListViewModel
    {
        #region Fields

        /// <summary>
        /// Defines the selected employee.
        /// </summary>
        private EmployeeDto selectedEmployee;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeeListViewModel"/> class.
        /// </summary>
        public EmployeeListViewModel()
        {
            if (DesignerProperties.GetIsInDesignMode(new DependencyObject()))
            {
                return;
            }

            using (EmployeeServiceClient serviceClient = new EmployeeServiceClient())
            {
                IEnumerable<EmployeeDto> allEmployees = serviceClient.GetAllAsync().Result;
                this.Employees = new ObservableCollection<EmployeeDto>(allEmployees);
            }

            this.DeleteCommand = new RelayCommand(this.OnDelete, this.CanDelete);
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the delete command.
        /// </summary>
        /// <value>
        /// The delete command.
        /// </value>
        public RelayCommand DeleteCommand
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the employees.
        /// </summary>
        /// <value>
        /// The employees.
        /// </value>
        public ObservableCollection<EmployeeDto> Employees
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the selected employee.
        /// </summary>
        /// <value>
        /// The selected employee.
        /// </value>
        public EmployeeDto SelectedEmployee
        {
            get
            {
                return this.selectedEmployee;
            }

            set
            {
                this.selectedEmployee = value;
                this.DeleteCommand.RaiseCanExecuteChanged();
            }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Determines whether the user can remove the selected employee.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if the user can remove the selected employee; otherwise, <c>false</c>.
        /// </returns>
        private bool CanDelete()
        {
            return (this.SelectedEmployee != null);
        }

        /// <summary>
        /// Called when the user requests to remove the selected employee.
        /// </summary>
        private void OnDelete()
        {
            this.Employees.Remove(this.SelectedEmployee);
        }

        #endregion Methods
    }
}