// ----------------------------------------------------------------------------
// <copyright file="Track.cs" company="SOGETI Spain">
//     Copyright © 2016 SOGETI Spain. All rights reserved.
//     MVVM Course by Óscar Fernández González a.k.a. Osc@rNET.
// </copyright>
// ----------------------------------------------------------------------------
namespace SogetiSpain.MvvmCourse.Domain
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Represents a track entity.
    /// </summary>
    public class Track : BaseEntity<Track, int>
    {
        #region Properties

        /// <summary>
        /// Gets or sets the bytes.
        /// </summary>
        /// <value>
        /// The bytes.
        /// </value>
        public virtual long? Bytes
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the composer.
        /// </summary>
        /// <value>
        /// The composer.
        /// </value>
        [StringLength(220)]
        public virtual string Composer
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the genre.
        /// </summary>
        /// <value>
        /// The genre.
        /// </value>
        [Required]
        public virtual Genre Genre
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the type of the media.
        /// </summary>
        /// <value>
        /// The type of the media.
        /// </value>
        [Required]
        public virtual MediaType MediaType
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the milliseconds.
        /// </summary>
        /// <value>
        /// The milliseconds.
        /// </value>
        public virtual int Milliseconds
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [Required]
        [StringLength(200)]
        public virtual string Name
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the unit price.
        /// </summary>
        /// <value>
        /// The unit price.
        /// </value>
        public virtual decimal UnitPrice
        {
            get;
            set;
        }

        #endregion Properties
    }
}