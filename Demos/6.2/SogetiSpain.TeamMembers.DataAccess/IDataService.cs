// ----------------------------------------------------------------------------
// <copyright file="IDataService.cs" company="SOGETI Spain">
//     Copyright © 2016 SOGETI Spain. All rights reserved.
//     MVVM Course by Óscar Fernández González a.k.a. Osc@rNET.
// </copyright>
// ----------------------------------------------------------------------------
namespace SogetiSpain.TeamMembers.DataAccess
{
    using System;
    using System.Collections.Generic;
    using Model;

    /// <summary>
    /// Defines the contract for a data service.
    /// </summary>
    public interface IDataService : IDisposable
    {
        #region Methods

        /// <summary>
        /// Deletes a team member.
        /// </summary>
        /// <param name="teamMemberId">The team member identifier.</param>
        void DeleteTeamMeamber(int teamMemberId);

        /// <summary>
        /// Gets all team members.
        /// </summary>
        /// <returns>
        /// All retrieved team members.
        /// </returns>
        IEnumerable<SearchItem> GetAllTeamMembers();

        /// <summary>
        /// Gets a team member by its identifier.
        /// </summary>
        /// <param name="teamMemberId">The team member identifier.</param>
        /// <returns>
        /// The retrieved team member.
        /// </returns>
        TeamMember GetTeamMemberById(int teamMemberId);

        /// <summary>
        /// Saves a team member.
        /// </summary>
        /// <param name="teamMember">The team member.</param>
        void SaveTeamMember(TeamMember teamMember);

        #endregion Methods
    }
}