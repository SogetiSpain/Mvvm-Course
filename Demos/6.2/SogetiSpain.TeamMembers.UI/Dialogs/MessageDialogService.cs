// ----------------------------------------------------------------------------
// <copyright file="MessageDialogService.cs" company="SOGETI Spain">
//     Copyright © 2016 SOGETI Spain. All rights reserved.
//     MVVM Course by Óscar Fernández González a.k.a. Osc@rNET.
// </copyright>
// ----------------------------------------------------------------------------
namespace SogetiSpain.TeamMembers.UI.Dialogs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;

    /// <summary>
    /// Represents the message dialog service.
    /// </summary>
    public class MessageDialogService : IMessageDialogService
    {
        #region Methods

        /// <summary>
        /// Shows the yes no dialog.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="message">The message.</param>
        /// <returns>
        /// The result of the dialog.
        /// </returns>
        public MessageDialogResult ShowYesNoDialog(string title, string message)
        {
            return new YesNoDialog(title, message)
            {
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                Owner = App.Current.MainWindow
            }.ShowDialog().GetValueOrDefault()
        ? MessageDialogResult.Yes
        : MessageDialogResult.No;
        }

        #endregion Methods
    }
}