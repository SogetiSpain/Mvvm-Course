// ----------------------------------------------------------------------------
// <copyright file="ITeamMemberEditViewModel.cs" company="SOGETI Spain">
//     Copyright © 2016 SOGETI Spain. All rights reserved.
//     MVVM Course by Óscar Fernández González a.k.a. Osc@rNET.
// </copyright>
// ----------------------------------------------------------------------------
namespace SogetiSpain.TeamMembers.UI.ViewModels
{
    using SogetiSpain.TeamMembers.UI.Models;

    /// <summary>
    /// Defines the contract of the view model for team member edit view.
    /// </summary>
    public interface ITeamMemberEditViewModel
    {
        #region Properties

        /// <summary>
        /// Gets the team member.
        /// </summary>
        /// <value>
        /// The team member.
        /// </value>
        WrappedTeamMember TeamMember
        {
            get;
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Loads the specified team member.
        /// </summary>
        /// <param name="teamMemberId">The team member identifier.</param>
        void Load(int? teamMemberId);

        #endregion Methods
    }
}