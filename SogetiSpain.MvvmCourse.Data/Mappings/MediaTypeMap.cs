// ----------------------------------------------------------------------------
// <copyright file="MediaTypeMap.cs" company="SOGETI Spain">
//     Copyright © 2016 SOGETI Spain. All rights reserved.
//     MVVM Course by Óscar Fernández González a.k.a. Osc@rNET.
// </copyright>
// ----------------------------------------------------------------------------
namespace SogetiSpain.MvvmCourse.Data
{
    using Domain;
    using FluentNHibernate.Mapping;

    /// <summary>
    /// Represents the map for the media type entity.
    /// </summary>
    public sealed class MediaTypeMap : ClassMap<MediaType>
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MediaTypeMap"/> class.
        /// </summary>
        public MediaTypeMap()
        {
            this.Table("MEDIATYPE");

            this.Id(e => e.Id, "MEDIATYPEID").GeneratedBy.Sequence("SEQ_MEDIATYPE");
            this.Map(e => e.Name, "NAME").Length(120).Not.Nullable();

            this.DynamicInsert();
            this.DynamicUpdate();
        }

        #endregion Constructors
    }
}