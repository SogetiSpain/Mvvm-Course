// ----------------------------------------------------------------------------
// <copyright file="INavigationViewModel.cs" company="SOGETI Spain">
//     Copyright © 2016 SOGETI Spain. All rights reserved.
//     MVVM Course by Óscar Fernández González a.k.a. Osc@rNET.
// </copyright>
// ----------------------------------------------------------------------------
namespace SogetiSpain.TeamMembers.UI.ViewModels
{
    /// <summary>
    /// Defines the contract of the view model for navigation view.
    /// </summary>
    public interface INavigationViewModel
    {
        #region Methods

        /// <summary>
        /// Called when load.
        /// </summary>
        void OnLoad();

        #endregion Methods
    }
}