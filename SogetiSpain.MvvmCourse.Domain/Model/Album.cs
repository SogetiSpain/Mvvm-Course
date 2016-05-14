// ----------------------------------------------------------------------------
// <copyright file="Album.cs" company="SOGETI Spain">
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
    /// Represents an album entity.
    /// </summary>
    public class Album : BaseEntity<Album, int>
    {
        #region Fields

        /// <summary>
        /// Defines the tracks that the album has.
        /// </summary>
        private readonly ICollection<Track> tracks = new HashSet<Track>();

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        [Required]
        [StringLength(160)]
        public virtual string Title
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the tracks that the album has.
        /// </summary>
        /// <value>
        /// The tracks that the album has.
        /// </value>
        public virtual IEnumerable<Track> Tracks
        {
            get
            {
                return new ReadOnlyCollection<Track>(this.tracks.ToList());
            }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Adds the specified track.
        /// </summary>
        /// <param name="track">The track.</param>
        public virtual void Add(Track track)
        {
            if (track == null)
            {
                throw new ArgumentNullException();
            }

            if (this.tracks.Contains(track))
            {
                throw new InvalidOperationException("The album already has this track!");
            }

            this.tracks.Add(track);
        }

        /// <summary>
        /// Removes the specified track.
        /// </summary>
        /// <param name="track">The track.</param>
        /// <returns>
        ///   <c>true</c> if the track was successfully removed from the collection; otherwise, <c>false</c>.
        /// </returns>
        public virtual bool Remove(Track track)
        {
            if (track == null)
            {
                throw new ArgumentNullException();
            }

            if (!this.tracks.Contains(track))
            {
                throw new InvalidOperationException("The album hasn't this track!");
            }

            return this.tracks.Remove(track);
        }

        /// <summary>
        /// Removes the specified track.
        /// </summary>
        /// <param name="trackId">The track identifier.</param>
        /// <returns>
        ///   <c>true</c> if the track was successfully removed from the collection; otherwise, <c>false</c>.
        /// </returns>
        public virtual bool RemoveTrack(int trackId)
        {
            Track trackToRemove = this.tracks.Where(track => (track.Id == trackId)).SingleOrDefault();
            return this.Remove(trackToRemove);
        }

        #endregion Methods
    }
}