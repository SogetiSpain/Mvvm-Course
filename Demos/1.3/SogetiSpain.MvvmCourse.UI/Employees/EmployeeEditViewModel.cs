// ----------------------------------------------------------------------------
// <copyright file="EmployeeEditViewModel.cs" company="SOGETI Spain">
//     Copyright © 2016 SOGETI Spain. All rights reserved.
//     MVVM Course by Óscar Fernández González a.k.a. Osc@rNET.
// </copyright>
// ----------------------------------------------------------------------------
namespace SogetiSpain.MvvmCourse.UI.Employees
{
    using System.ComponentModel;
    using System.Windows.Input;
    using WebServices;
    using WebServices.Client.EmployeeServiceClient;

    /// <summary>
    /// Represents the view model for Employee edit view.
    /// </summary>
    public sealed class EmployeeEditViewModel : INotifyPropertyChanged
    {
        #region Fields

        /// <summary>
        /// Defines the employee.
        /// </summary>
        private EmployeeDto employee;

        /// <summary>
        /// Defines the value indicating whether the view is enabled.
        /// </summary>
        private bool isEnabled;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeeEditViewModel"/> class.
        /// </summary>
        public EmployeeEditViewModel()
        {
            this.SaveCommand = new RelayCommand(this.OnSave);
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
        /// Gets or sets the employee.
        /// </summary>
        /// <value>
        /// The employee.
        /// </value>
        public EmployeeDto Employee
        {
            get
            {
                return this.employee;
            }

            set
            {
                if (this.employee != value)
                {
                    this.employee = value;
                    this.PropertyChanged(this, new PropertyChangedEventArgs(nameof(this.Employee)));
                }
            }
        }

        /// <summary>
        /// Gets or sets the employee identifier.
        /// </summary>
        /// <value>
        /// The employee identifier.
        /// </value>
        public int EmployeeId
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the view is enabled.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the view is enabled; otherwise, <c>false</c>.
        /// </value>
        public bool IsEnabled
        {
            get
            {
                return this.isEnabled;
            }

            set
            {
                if (this.isEnabled != value)
                {
                    this.isEnabled = value;
                    this.PropertyChanged(this, new PropertyChangedEventArgs(nameof(this.IsEnabled)));
                }
            }
        }

        /// <summary>
        /// Gets the save command.
        /// </summary>
        /// <value>
        /// The save command.
        /// </value>
        public ICommand SaveCommand
        {
            get;
            private set;
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Loads an employee.
        /// </summary>
        public async void LoadEmployee()
        {
            this.IsEnabled = false;

            this.Employee = null;
            using (EmployeeServiceClient serviceClient = new EmployeeServiceClient())
            {
                this.Employee = await serviceClient.GetByIdAsync(this.EmployeeId);
            }

            this.IsEnabled = true;
        }

        /// <summary>
        /// Called when the user requests save data.
        /// </summary>
        private async void OnSave()
        {
            this.IsEnabled = false;

            using (EmployeeServiceClient serviceClient = new EmployeeServiceClient())
            {
                await serviceClient.UpdateAsync(this.employee);
            }

            this.IsEnabled = true;
        }

        #endregion Methods
    }
}