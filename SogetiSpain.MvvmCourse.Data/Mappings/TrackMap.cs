// ----------------------------------------------------------------------------
// <copyright file="TrackMap.cs" company="SOGETI Spain">
//     Copyright © 2016 SOGETI Spain. All rights reserved.
//     MVVM Course by Óscar Fernández González a.k.a. Osc@rNET.
// </copyright>
// ----------------------------------------------------------------------------
namespace SogetiSpain.MvvmCourse.Data
{
    using Domain;
    using FluentNHibernate.Mapping;

    /// <summary>
    /// Represents the map for the track entity.
    /// </summary>
    public sealed class TrackMap : ClassMap<Track>
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TrackMap"/> class.
        /// </summary>
        public TrackMap()
        {
            this.Table("TRACK");

            this.Id(e => e.Id, "TRACKID").GeneratedBy.Sequence("SEQ_TRACK");
            this.Map(e => e.Name, "NAME").Length(200).Not.Nullable();
            this.Map(e => e.Composer, "COMPOSER").Length(220);
            this.Map(e => e.Milliseconds, "MILLISECONDS").Not.Nullable();
            this.Map(e => e.Bytes, "BYTES");
            this.Map(e => e.UnitPrice, "UNITPRICE").Not.Nullable();

            this.References(e => e.MediaType, "MEDIATYPEID");
            this.References(e => e.Genre, "GENREID");

            this.DynamicInsert();
            this.DynamicUpdate();
        }

        #endregion Constructors
    }
}