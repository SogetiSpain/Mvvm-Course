namespace SogetiSpain.DI_IoC.ViewLayer
{
    using System.Windows;
    using SogetiSpain.DI_IoC.PresentationLayer;

    public partial class EmployeeWindow : Window
    {
        public EmployeeWindow(EmployeeWindowViewModel viewModel)
        {
            this.InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}