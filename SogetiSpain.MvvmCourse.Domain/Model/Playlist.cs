// ----------------------------------------------------------------------------
// <copyright file="Playlist.cs" company="SOGETI Spain">
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
    /// Represents a playlist entity.
    /// </summary>
    public class Playlist : BaseEntity<Playlist, int>
    {
        #region Fields

        /// <summary>
        /// Defines the tracks that the playlist has.
        /// </summary>
        private readonly ICollection<Track> tracks = new HashSet<Track>();

        #endregion Fields

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

        /// <summary>
        /// Gets the tracks that the playlist has.
        /// </summary>
        /// <value>
        /// The tracks that the playlist has.
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
                throw new InvalidOperationException("The playlist already has this track!");
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
                throw new InvalidOperationException("The playlist hasn't this track!");
            }

            return this.tracks.Remove(track);
        }

        /// <summary>
        /// Removes all tracks.
        /// </summary>
        public virtual void RemoveAllTracks()
        {
            this.tracks.Clear();
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