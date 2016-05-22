// ----------------------------------------------------------------------------
// <copyright file="TeamMemberDataProvider.cs" company="SOGETI Spain">
//     Copyright © 2016 SOGETI Spain. All rights reserved.
//     MVVM Course by Óscar Fernández González a.k.a. Osc@rNET.
// </copyright>
// ----------------------------------------------------------------------------
namespace SogetiSpain.TeamMembers.UI.DataProviders
{
    using System;
    using DataAccess;
    using Model;

    /// <summary>
    /// Represents the team member data provider.
    /// </summary>
    public class TeamMemberDataProvider : ITeamMemberDataProvider
    {
        #region Fields

        /// <summary>
        /// Defines the data service creator.
        /// </summary>
        private readonly Func<IDataService> dataServiceCreator;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TeamMemberDataProvider"/> class.
        /// </summary>
        /// <param name="dataServiceCreator">The data service creator.</param>
        public TeamMemberDataProvider(Func<IDataService> dataServiceCreator)
        {
            this.dataServiceCreator = dataServiceCreator;
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Deletes a team member.
        /// </summary>
        /// <param name="teamMemberId">The team member identifier.</param>
        public void DeleteTeamMeamber(int teamMemberId)
        {
            using (IDataService dataService = this.dataServiceCreator())
            {
                dataService.DeleteTeamMeamber(teamMemberId);
            }
        }

        /// <summary>
        /// Gets a team member by its identifier.
        /// </summary>
        /// <param name="teamMemberId">The team member identifier.</param>
        /// <returns>
        /// The retrieved team member.
        /// </returns>
        public TeamMember GetTeamMemberById(int teamMemberId)
        {
            using (IDataService dataService = this.dataServiceCreator())
            {
                return dataService.GetTeamMemberById(teamMemberId);
            }
        }

        /// <summary>
        /// Saves a team member.
        /// </summary>
        /// <param name="teamMember">The team member.</param>
        public void SaveTeamMember(TeamMember teamMember)
        {
            using (IDataService dataService = this.dataServiceCreator())
            {
                dataService.SaveTeamMember(teamMember);
            }
        }

        #endregion Methods
    }
}