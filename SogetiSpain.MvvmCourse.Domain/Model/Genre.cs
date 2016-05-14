// ----------------------------------------------------------------------------
// <copyright file="Genre.cs" company="SOGETI Spain">
//     Copyright © 2016 SOGETI Spain. All rights reserved.
//     MVVM Course by Óscar Fernández González a.k.a. Osc@rNET.
// </copyright>
// ----------------------------------------------------------------------------
namespace SogetiSpain.MvvmCourse.Domain
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Represents a genre entity.
    /// </summary>
    public class Genre : BaseEntity<Genre, int>
    {
        #region Properties

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [Required]
        [StringLength(120)]
        public virtual string Name
        {
            get;
            set;
        }

        #endregion Properties
    }
}