// ----------------------------------------------------------------------------
// <copyright file="NavigationDataProvider.cs" company="SOGETI Spain">
//     Copyright © 2016 SOGETI Spain. All rights reserved.
//     MVVM Course by Óscar Fernández González a.k.a. Osc@rNET.
// </copyright>
// ----------------------------------------------------------------------------
namespace SogetiSpain.TeamMembers.UI.DataProviders
{
    using System;
    using System.Collections.Generic;
    using DataAccess;
    using Model;

    /// <summary>
    /// Represents the navigation data provider.
    /// </summary>
    public class NavigationDataProvider : INavigationDataProvider
    {
        #region Fields

        /// <summary>
        /// Defines the data service creator.
        /// </summary>
        private readonly Func<IDataService> dataServiceCreator;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="NavigationDataProvider"/> class.
        /// </summary>
        /// <param name="dataServiceCreator">The data service creator.</param>
        public NavigationDataProvider(Func<IDataService> dataServiceCreator)
        {
            this.dataServiceCreator = dataServiceCreator;
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Gets all team members.
        /// </summary>
        /// <returns>
        /// All retrieved team members.
        /// </returns>
        public IEnumerable<SearchItem> GetAllTeamMembers()
        {
            using (IDataService dataService = this.dataServiceCreator())
            {
                return dataService.GetAllTeamMembers();
            }
        }

        #endregion Methods
    }
}