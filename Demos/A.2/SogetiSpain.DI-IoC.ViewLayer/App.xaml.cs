namespace SogetiSpain.DI_IoC.ViewLayer
{
    using System.Windows;
    using PresentationLayer;
    using ServiceClientLayer;
    using Microsoft.Practices.Unity;

    public partial class App : Application
    {
        private IUnityContainer container;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            ////this.ComposeObjects();
            this.ConfigureContainer();
            Application.Current.MainWindow.Show();
        }

        private void ConfigureContainer()
        {
            this.container = new UnityContainer();
            this.container.RegisterType<IServiceCatalog, ServiceCatalog>();
            Application.Current.MainWindow = this.container.Resolve<EmployeeWindow>();
        }

        private void ComposeObjects()
        {
            IServiceCatalog serviceCatalog = new ServiceCatalog();
            EmployeeWindowViewModel viewModel = new EmployeeWindowViewModel(serviceCatalog);
            Application.Current.MainWindow = new EmployeeWindow(viewModel);
        }
    }
}
