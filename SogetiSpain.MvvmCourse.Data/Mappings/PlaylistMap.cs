// ----------------------------------------------------------------------------
// <copyright file="PlaylistMap.cs" company="SOGETI Spain">
//     Copyright © 2016 SOGETI Spain. All rights reserved.
//     MVVM Course by Óscar Fernández González a.k.a. Osc@rNET.
// </copyright>
// ----------------------------------------------------------------------------
namespace SogetiSpain.MvvmCourse.Data
{
    using Domain;
    using FluentNHibernate.Mapping;

    /// <summary>
    /// Represents the map for the playlist entity.
    /// </summary>
    public sealed class PlaylistMap : ClassMap<Playlist>
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PlaylistMap"/> class.
        /// </summary>
        public PlaylistMap()
        {
            this.Table("PLAYLIST");

            this.Id(e => e.Id, "PLAYLISTID").GeneratedBy.Sequence("SEQ_PLAYLIST");
            this.Map(e => e.Name, "NAME").Length(120).Not.Nullable();

            this.HasMany<Track>(e => e.Tracks)
                .Access.CamelCaseField()
                .AsSet()
                .BatchSize(25)
                .Cascade.AllDeleteOrphan()
                .ForeignKeyConstraintName("FK_PLAYLISTTRACKPLAYLISTID")
                .KeyColumn("PLAYLISTID")
                .LazyLoad()
                .Table("PLAYLISTTRACK");

            this.DynamicInsert();
            this.DynamicUpdate();
        }

        #endregion Constructors
    }
}