﻿// ----------------------------------------------------------------------------
// <copyright file="ArtistRepository.cs" company="SOGETI Spain">
//     Copyright © 2016 SOGETI Spain. All rights reserved.
//     MVVM Course by Óscar Fernández González a.k.a. Osc@rNET.
// </copyright>
// ----------------------------------------------------------------------------
namespace SogetiSpain.MvvmCourse.Data
{
    using Domain;

    /// <summary>
    /// Represents the repository of the artist entity.
    /// </summary>
    public sealed class ArtistRepository : NHibernateRepository<Artist, int>, IArtistRepository
    {
    }
}