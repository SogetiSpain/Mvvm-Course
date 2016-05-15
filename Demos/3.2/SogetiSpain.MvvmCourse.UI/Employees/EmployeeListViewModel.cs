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
    public sealed class EmployeeListViewModel : INotifyPropertyChanged
    {
        #region Fields

        /// <summary>
        /// Defines the employee collection.
        /// </summary>
        private ObservableCollection<EmployeeDto> employees;

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
            this.DeleteCommand = new RelayCommand(this.OnDelete, this.CanDelete);
        }

        #endregion Constructors

        #region Events

        /// <summary>
        /// Notifies clients that a property value has changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        #endregion Events

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
            get
            {
                return this.employees;
            }

            set
            {
                if (this.employees != value)
                {
                    this.employees = value;
                    this.PropertyChanged(this, new PropertyChangedEventArgs(nameof(this.Employees)));
                }
            }
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
                if (this.selectedEmployee != value)
                {
                    this.selectedEmployee = value;
                    this.DeleteCommand.RaiseCanExecuteChanged();
                    this.PropertyChanged(this, new PropertyChangedEventArgs(nameof(this.SelectedEmployee)));
                }
            }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Loads the employees.
        /// </summary>
        public async void LoadEmployees()
        {
            if (DesignerProperties.GetIsInDesignMode(new DependencyObject()))
            {
                return;
            }

            using (EmployeeServiceClient serviceClient = new EmployeeServiceClient())
            {
                IEnumerable<EmployeeDto> allEmployees = await serviceClient.GetAllAsync();
                this.Employees = new ObservableCollection<EmployeeDto>(allEmployees);
            }
        }

        /// <summary>
        /// Determines whether the user can remove the selected employee.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if the user can remove the selected employee; otherwise, <c>false</c>.
        /// </returns>
        private bool CanDelete()
        {
            return this.SelectedEmployee != null;
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