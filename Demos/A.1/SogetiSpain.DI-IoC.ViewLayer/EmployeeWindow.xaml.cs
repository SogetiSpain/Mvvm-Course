namespace SogetiSpain.DI_IoC.ViewLayer
{
    using System.Windows;
    using SogetiSpain.DI_IoC.PresentationLayer;

    public partial class EmployeeWindow : Window
    {
        public EmployeeWindow()
        {
            this.InitializeComponent();
            this.DataContext = new EmployeeWindowViewModel();
        }
    }
}