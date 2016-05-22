// ----------------------------------------------------------------------------
// <copyright file="YesNoDialog.xaml.cs" company="SOGETI Spain">
//     Copyright © 2016 SOGETI Spain. All rights reserved.
//     MVVM Course by Óscar Fernández González a.k.a. Osc@rNET.
// </copyright>
// ----------------------------------------------------------------------------
namespace SogetiSpain.TeamMembers.UI.Dialogs
{
    using System.Windows;

    /// <summary>
    /// Interaction logic for main window.
    /// </summary>
    public partial class YesNoDialog : Window
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="YesNoDialog"/> class.
        /// </summary>
        public YesNoDialog()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="YesNoDialog"/> class.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="message">The message.</param>
        public YesNoDialog(string title, string message)
            : this()
        {
            this.Title = title;
            this.MessageTextBlock.Text = message;
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Called when the no button is clicked.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void OnNoButtonClicked(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        /// <summary>
        /// Called when the yes button is clicked.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void OnYesButtonClicked(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        #endregion Methods
    }
}