// ----------------------------------------------------------------------------
// <copyright file="App.xaml.cs" company="SOGETI Spain">
//     Copyright © 2016 SOGETI Spain. All rights reserved.
//     MVVM Course by Óscar Fernández González a.k.a. Osc@rNET.
// </copyright>
// ----------------------------------------------------------------------------
namespace SogetiSpain.TeamMembers.UI
{
    using System.Windows;
    using Microsoft.Practices.ServiceLocation;
    using Views;

    /// <summary>
    /// Interaction logic for App view.
    /// </summary>
    public partial class App : Application
    {
        #region Methods

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Application.Startup" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.StartupEventArgs" /> that contains the event data.</param>
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            Bootstrapper.Run();

            Application.Current.MainWindow = ServiceLocator.Current.GetInstance<MainWindow>();
            Application.Current.MainWindow.Show();
        }

        #endregion Methods
    }
}