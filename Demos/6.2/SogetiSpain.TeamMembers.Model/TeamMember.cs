// ----------------------------------------------------------------------------
// <copyright file="TeamMember.cs" company="SOGETI Spain">
//     Copyright © 2016 SOGETI Spain. All rights reserved.
//     MVVM Course by Óscar Fernández González a.k.a. Osc@rNET.
// </copyright>
// ----------------------------------------------------------------------------
namespace SogetiSpain.TeamMembers.Model
{
    using System;

    /// <summary>
    /// Represents a team member.
    /// </summary>
    public class TeamMember
    {
        #region Properties

        /// <summary>
        /// Gets or sets the birthday.
        /// </summary>
        /// <value>
        /// The birthday.
        /// </value>
        public DateTime? Birthday
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        public string FirstName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is developer.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is developer; otherwise, <c>false</c>.
        /// </value>
        public bool IsDeveloper
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        public string LastName
        {
            get;
            set;
        }

        #endregion Properties
    }
}