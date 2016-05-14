// ----------------------------------------------------------------------------
// <copyright file="GenreMap.cs" company="SOGETI Spain">
//     Copyright © 2016 SOGETI Spain. All rights reserved.
//     MVVM Course by Óscar Fernández González a.k.a. Osc@rNET.
// </copyright>
// ----------------------------------------------------------------------------
namespace SogetiSpain.MvvmCourse.Data
{
    using Domain;
    using FluentNHibernate.Mapping;

    /// <summary>
    /// Represents the map for the genre entity.
    /// </summary>
    public sealed class GenreMap : ClassMap<Genre>
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="GenreMap"/> class.
        /// </summary>
        public GenreMap()
        {
            this.Table("GENRE");

            this.Id(e => e.Id, "GENREID").GeneratedBy.Sequence("SEQ_GENRE");
            this.Map(e => e.Name, "NAME").Length(120).Not.Nullable();

            this.DynamicInsert();
            this.DynamicUpdate();
        }

        #endregion Constructors
    }
}