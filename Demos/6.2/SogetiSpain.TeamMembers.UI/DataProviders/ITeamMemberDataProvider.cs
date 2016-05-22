// ----------------------------------------------------------------------------
// <copyright file="ITeamMemberDataProvider.cs" company="SOGETI Spain">
//     Copyright © 2016 SOGETI Spain. All rights reserved.
//     MVVM Course by Óscar Fernández González a.k.a. Osc@rNET.
// </copyright>
// ----------------------------------------------------------------------------
namespace SogetiSpain.TeamMembers.UI.DataProviders
{
    using Model;

    /// <summary>
    /// Defines the contract for the team member data provider.
    /// </summary>
    public interface ITeamMemberDataProvider
    {
        #region Methods

        /// <summary>
        /// Deletes a team member.
        /// </summary>
        /// <param name="teamMemberId">The team member identifier.</param>
        void DeleteTeamMeamber(int teamMemberId);

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