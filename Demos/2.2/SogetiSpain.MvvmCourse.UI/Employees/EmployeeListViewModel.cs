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
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the employees.
        /// </summary>
        /// <value>
        /// The employees.
        /// </value>
        public ObservableCollection<EmployeeDto> Employees
        {
            get;
            private set;
        }

        #endregion Properties
    }
}