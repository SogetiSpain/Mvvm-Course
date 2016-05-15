// ----------------------------------------------------------------------------
// <copyright file="MainWindowViewModel.cs" company="SOGETI Spain">
//     Copyright © 2016 SOGETI Spain. All rights reserved.
//     MVVM Course by Óscar Fernández González a.k.a. Osc@rNET.
// </copyright>
// ----------------------------------------------------------------------------
namespace SogetiSpain.MvvmCourse.UI
{
    using SogetiSpain.MvvmCourse.UI.Employees;

    /// <summary>
    /// Represents the view model for main window.
    /// </summary>
    public sealed class MainWindowViewModel
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindowViewModel"/> class.
        /// </summary>
        public MainWindowViewModel()
        {
            this.EmployeeViewModel = new EmployeeListViewModel();
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets the employee view model.
        /// </summary>
        /// <value>
        /// The employee view model.
        /// </value>
        public object EmployeeViewModel
        {
            get;
            set;
        }

        #endregion Properties
    }
}