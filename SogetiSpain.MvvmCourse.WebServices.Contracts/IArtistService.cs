// ----------------------------------------------------------------------------
// <copyright file="IArtistService.cs" company="SOGETI Spain">
//     Copyright © 2016 SOGETI Spain. All rights reserved.
//     MVVM Course by Óscar Fernández González a.k.a. Osc@rNET.
// </copyright>
// ----------------------------------------------------------------------------
namespace SogetiSpain.MvvmCourse.WebServices
{
    using System.Collections.Generic;
    using System.ServiceModel;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the contract for the artist Web Service.
    /// </summary>
    [ServiceContract]
    public interface IArtistService
    {
        #region Methods

        /// <summary>
        /// Gets all artists.
        /// </summary>
        /// <returns>
        /// The all retrieved artists.
        /// </returns>
        [OperationContract]
        Task<IEnumerable<ArtistDto>> GetAllAsync();

        /// <summary>
        /// Gets an artist by its identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// The retrieved artist.
        /// </returns>
        [OperationContract]
        Task<ArtistDto> GetByIdAsync(int id);

        #endregion Methods
    }
}