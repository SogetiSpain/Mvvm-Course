// ----------------------------------------------------------------------------
// <copyright file="IMessageDialogService.cs" company="SOGETI Spain">
//     Copyright © 2016 SOGETI Spain. All rights reserved.
//     MVVM Course by Óscar Fernández González a.k.a. Osc@rNET.
// </copyright>
// ----------------------------------------------------------------------------
namespace SogetiSpain.TeamMembers.UI.Dialogs
{
    /// <summary>
    /// Defines the contract of the message dialog service.
    /// </summary>
    public interface IMessageDialogService
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
        MessageDialogResult ShowYesNoDialog(string title, string message);

        #endregion Methods
    }
}
