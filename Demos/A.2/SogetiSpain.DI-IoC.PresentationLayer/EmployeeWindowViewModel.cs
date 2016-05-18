namespace SogetiSpain.DI_IoC.PresentationLayer
{
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using ServiceClientLayer;
    using SharedObjects;

    public sealed class EmployeeWindowViewModel : INotifyPropertyChanged
    {
        private readonly IServiceCatalog serviceCatalog;
        private ObservableCollection<Employee> employees;

        public EmployeeWindowViewModel(IServiceCatalog serviceCatalog)
        {
            this.serviceCatalog = serviceCatalog;
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        public ObservableCollection<Employee> Employees
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

        public void LoadEmployees()
        {
            this.Employees =
                new ObservableCollection<Employee>(
                    this.serviceCatalog.GetAllEmployees());
        }
    }
}