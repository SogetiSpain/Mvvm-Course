// ----------------------------------------------------------------------------
// <copyright file="Artist.cs" company="SOGETI Spain">
//     Copyright © 2016 SOGETI Spain. All rights reserved.
//     MVVM Course by Óscar Fernández González a.k.a. Osc@rNET.
// </copyright>
// ----------------------------------------------------------------------------
namespace SogetiSpain.MvvmCourse.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    /// <summary>
    /// Represents an artist entity.
    /// </summary>
    public class Artist : BaseEntity<Artist, int>
    {
        #region Fields

        /// <summary>
        /// Defines the albums that the artist has.
        /// </summary>
        private readonly ICollection<Album> albums = new HashSet<Album>();

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets the albums that the artist has.
        /// </summary>
        /// <value>
        /// The albums that the artist has.
        /// </value>
        public virtual IEnumerable<Album> Albums
        {
            get
            {
                return new ReadOnlyCollection<Album>(this.albums.ToList());
            }
        }

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

        #region Methods

        /// <summary>
        /// Adds the specified album.
        /// </summary>
        /// <param name="album">The album.</param>
        public virtual void Add(Album album)
        {
            if (album == null)
            {
                throw new ArgumentNullException();
            }

            if (this.albums.Contains(album))
            {
                throw new InvalidOperationException("The artist already has this album!");
            }

            this.albums.Add(album);
        }

        /// <summary>
        /// Removes the specified album.
        /// </summary>
        /// <param name="album">The album.</param>
        /// <returns>
        ///   <c>true</c> if the album was successfully removed from the collection; otherwise, <c>false</c>.
        /// </returns>
        public virtual bool Remove(Album album)
        {
            if (album == null)
            {
                throw new ArgumentNullException();
            }

            if (!this.albums.Contains(album))
            {
                throw new InvalidOperationException("The artist hasn't this album!");
            }

            return this.albums.Remove(album);
        }

        /// <summary>
        /// Removes the specified album.
        /// </summary>
        /// <param name="albumId">The album identifier.</param>
        /// <returns>
        ///   <c>true</c> if the album was successfully removed from the collection; otherwise, <c>false</c>.
        /// </returns>
        public virtual bool RemoveAlbum(int albumId)
        {
            Album albumToRemove = this.albums.Where(album => (album.Id == albumId)).SingleOrDefault();
            return this.Remove(albumToRemove);
        }

        #endregion Methods
    }
}