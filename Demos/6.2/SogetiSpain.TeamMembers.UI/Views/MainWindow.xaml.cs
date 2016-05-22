// ----------------------------------------------------------------------------
// <copyright file="MainWindow.xaml.cs" company="SOGETI Spain">
//     Copyright © 2016 SOGETI Spain. All rights reserved.
//     MVVM Course by Óscar Fernández González a.k.a. Osc@rNET.
// </copyright>
// ----------------------------------------------------------------------------
namespace SogetiSpain.TeamMembers.UI.Views
{
    using System.Windows;
    using ViewModels;

    /// <summary>
    /// Interaction logic for main window.
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        public MainWindow(MainWindowViewModel viewModel)
            : this()
        {
            this.DataContext = viewModel;
        }

        #endregion Constructors
    }
}