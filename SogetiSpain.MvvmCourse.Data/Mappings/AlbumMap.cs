// ----------------------------------------------------------------------------
// <copyright file="AlbumMap.cs" company="SOGETI Spain">
//     Copyright © 2016 SOGETI Spain. All rights reserved.
//     MVVM Course by Óscar Fernández González a.k.a. Osc@rNET.
// </copyright>
// ----------------------------------------------------------------------------
namespace SogetiSpain.MvvmCourse.Data
{
    using Domain;
    using FluentNHibernate.Mapping;

    /// <summary>
    /// Represents the map for the album entity.
    /// </summary>
    public sealed class AlbumMap : ClassMap<Album>
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AlbumMap"/> class.
        /// </summary>
        public AlbumMap()
        {
            this.Table("ALBUM");

            this.Id(e => e.Id, "ALBUMID").GeneratedBy.Sequence("SEQ_ALBUM");
            this.Map(e => e.Title, "TITLE").Length(160).Not.Nullable();

            this.HasMany<Track>(e => e.Tracks)
                .Access.CamelCaseField()
                .AsSet()
                .BatchSize(25)
                .Cascade.AllDeleteOrphan()
                .ForeignKeyConstraintName("FK_TRACKALBUMID")
                .KeyColumn("ALBUMID")
                .LazyLoad();

            this.DynamicInsert();
            this.DynamicUpdate();
        }

        #endregion Constructors
    }
}