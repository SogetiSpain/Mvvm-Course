// ----------------------------------------------------------------------------
// <copyright file="ArtistMap.cs" company="SOGETI Spain">
//     Copyright © 2016 SOGETI Spain. All rights reserved.
//     MVVM Course by Óscar Fernández González a.k.a. Osc@rNET.
// </copyright>
// ----------------------------------------------------------------------------
namespace SogetiSpain.MvvmCourse.Data
{
    using Domain;
    using FluentNHibernate.Mapping;

    /// <summary>
    /// Represents the map for the artist entity.
    /// </summary>
    public sealed class ArtistMap : ClassMap<Artist>
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ArtistMap"/> class.
        /// </summary>
        public ArtistMap()
        {
            this.Table("ARTIST");

            this.Id(e => e.Id, "ARTISTID").GeneratedBy.Sequence("SEQ_ARTIST");
            this.Map(e => e.Name, "NAME").Length(120).Not.Nullable();

            this.HasMany<Album>(e => e.Albums)
                .Access.CamelCaseField()
                .AsSet()
                .BatchSize(25)
                .Cascade.AllDeleteOrphan()
                .ForeignKeyConstraintName("FK_ALBUMARTISTID")
                .KeyColumn("ARTISTID")
                .LazyLoad();

            this.DynamicInsert();
            this.DynamicUpdate();
        }

        #endregion Constructors
    }
}