// ----------------------------------------------------------------------------
// <copyright file="ArtistRepository.cs" company="SOGETI Spain">
//     Copyright © 2016 SOGETI Spain. All rights reserved.
//     MVVM Course by Óscar Fernández González a.k.a. Osc@rNET.
// </copyright>
// ----------------------------------------------------------------------------
namespace SogetiSpain.MvvmCourse.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using Domain;
    using NHibernate.Linq;

    /// <summary>
    /// Represents the repository of the artist entity.
    /// </summary>
    public sealed class ArtistRepository : NHibernateRepository<Artist, int>, IArtistRepository
    {
        public new IEnumerable<Artist> GetAll()
        {
            IQueryable<Artist> queryAllArtists =
                this.Session.Query<Artist>()
                    .FetchMany(artist => artist.Albums)
                    .ThenFetchMany(album => album.Tracks);

            return queryAllArtists.ToList();
        }
    }
}