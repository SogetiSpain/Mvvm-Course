// ----------------------------------------------------------------------------
// <copyright file="INavigationDataProvider.cs" company="SOGETI Spain">
//     Copyright © 2016 SOGETI Spain. All rights reserved.
//     MVVM Course by Óscar Fernández González a.k.a. Osc@rNET.
// </copyright>
// ----------------------------------------------------------------------------
namespace SogetiSpain.TeamMembers.UI.DataProviders
{
    using System.Collections.Generic;
    using Model;

    /// <summary>
    /// Defines the contract for the navigation data provider.
    /// </summary>
    public interface INavigationDataProvider
    {
        #region Methods

        /// <summary>
        /// Gets all team members.
        /// </summary>
        /// <returns>
        /// All retrieved team members.
        /// </returns>
        IEnumerable<SearchItem> GetAllTeamMembers();

        #endregion Methods
    }
}