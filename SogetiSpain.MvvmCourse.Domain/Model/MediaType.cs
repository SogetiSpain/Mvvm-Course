// ----------------------------------------------------------------------------
// <copyright file="MediaType.cs" company="SOGETI Spain">
//     Copyright © 2016 SOGETI Spain. All rights reserved.
//     MVVM Course by Óscar Fernández González a.k.a. Osc@rNET.
// </copyright>
// ----------------------------------------------------------------------------
namespace SogetiSpain.MvvmCourse.Domain
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Represents a media type entity.
    /// </summary>
    public class MediaType : BaseEntity<MediaType, int>
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