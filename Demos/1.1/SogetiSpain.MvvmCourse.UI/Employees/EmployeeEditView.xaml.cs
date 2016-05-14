// ----------------------------------------------------------------------------
// <copyright file="EmployeeEditView.xaml.cs" company="SOGETI Spain">
//     Copyright © 2016 SOGETI Spain. All rights reserved.
//     MVVM Course by Óscar Fernández González a.k.a. Osc@rNET.
// </copyright>
// ----------------------------------------------------------------------------
namespace SogetiSpain.MvvmCourse.UI.Employees
{
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using WebServices;
    using WebServices.Client.EmployeeServiceClient;

    /// <summary>
    /// Interaction logic for Employee edit view.
    /// </summary>
    public partial class EmployeeEditView : UserControl
    {
        #region Fields

        /// <summary>
        /// Defines the employee identifier property.
        /// </summary>
        private static readonly DependencyProperty EmployeeIdProperty =
            DependencyProperty.Register(
                "EmployeeId",
                typeof(int),
                typeof(EmployeeEditView),
                new PropertyMetadata(default(int)));

        /// <summary>
        /// Defines the employee.
        /// </summary>
        private EmployeeDto employee;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeeEditView"/> class.
        /// </summary>
        public EmployeeEditView()
        {
            this.InitializeComponent();
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets the employee identifier.
        /// </summary>
        /// <value>
        /// The employee identifier.
        /// </value>
        public int EmployeeId
        {
            get
            {
                return (int)this.GetValue(EmployeeEditView.EmployeeIdProperty);
            }

            set
            {
                this.SetValue(EmployeeEditView.EmployeeIdProperty, value);
            }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Called when this instance is loaded.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private async void OnLoaded(object sender, RoutedEventArgs e)
        {
            if (DesignerProperties.GetIsInDesignMode(this))
            {
                return;
            }

            this.Cursor = Cursors.Wait;
            this.IsEnabled = false;

            this.employee = null;
            using (EmployeeServiceClient serviceClient = new EmployeeServiceClient())
            {
                this.employee = await serviceClient.GetByIdAsync(this.EmployeeId);
            }

            this.Cursor = Cursors.None;
            this.IsEnabled = true;

            if (this.employee == null)
            {
                return;
            }

            FirstNameTextBox.Text = this.employee.FirstName;
            LastNameTextBox.Text = this.employee.LastName;
            TitleTextBox.Text = this.employee.Title;
            BirthDateDatePicker.SelectedDate = this.employee.BirthDate;
            HireDateDatePicker.SelectedDate = this.employee.HireDate;
            AddressTextBox.Text = this.employee.Address;
            CityTextBox.Text = this.employee.City;
            StateTextBox.Text = this.employee.State;
            CountryTextBox.Text = this.employee.Country;
            PostalCodeTextBox.Text = this.employee.PostalCode;
            PhoneTextBox.Text = this.employee.Phone;
            FaxTextBox.Text = this.employee.Fax;
        }

        /// <summary>
        /// Called when the user requests save data.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private async void OnSave(object sender, RoutedEventArgs e)
        {
            this.IsEnabled = false;

            // TODO: Validate input... etc...
            this.employee.FirstName = FirstNameTextBox.Text;
            this.employee.LastName = LastNameTextBox.Text;
            this.employee.Title = TitleTextBox.Text;
            this.employee.BirthDate = BirthDateDatePicker.SelectedDate;
            this.employee.HireDate = HireDateDatePicker.SelectedDate;
            this.employee.Address = AddressTextBox.Text;
            this.employee.City = CityTextBox.Text;
            this.employee.State = StateTextBox.Text;
            this.employee.Country = CountryTextBox.Text;
            this.employee.PostalCode = PostalCodeTextBox.Text;
            this.employee.Phone = PhoneTextBox.Text;
            this.employee.Fax = FaxTextBox.Text;

            using (EmployeeServiceClient serviceClient = new EmployeeServiceClient())
            {
                await serviceClient.UpdateAsync(this.employee);
            }

            this.IsEnabled = true;
        }

        #endregion Methods
    }
}