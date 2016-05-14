// ----------------------------------------------------------------------------
// <copyright file="ArtistService.svc.cs" company="SOGETI Spain">
//     Copyright © 2016 SOGETI Spain. All rights reserved.
//     MVVM Course by Óscar Fernández González a.k.a. Osc@rNET.
// </copyright>
// ----------------------------------------------------------------------------
namespace SogetiSpain.MvvmCourse.WebServices
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using Domain;
    using Microsoft.Practices.ServiceLocation;

    /// <summary>
    /// Represents the artist Web Service.
    /// </summary>
    public sealed class ArtistService : IArtistService
    {
        #region Fields

        /// <summary>
        /// Defines the artist repository.
        /// </summary>
        private readonly IArtistRepository artistRepository;

        /// <summary>
        /// Defines the mapper.
        /// </summary>
        private readonly IMapper mapper;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ArtistService"/> class.
        /// </summary>
        public ArtistService()
        {
            this.artistRepository = ServiceLocator.Current.GetInstance<IArtistRepository>();
            this.mapper = ServiceLocator.Current.GetInstance<IMapper>();
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Gets all artists.
        /// </summary>
        /// <returns>
        /// The all retrieved artists.
        /// </returns>
        public async Task<IEnumerable<ArtistDto>> GetAllAsync()
        {
            return await Task.Factory.StartNew(() =>
            {
                IEnumerable<Artist> artists = this.artistRepository.GetAll();
                IEnumerable<ArtistDto> artistDtos = artists.Select(
                    artist => this.mapper.Map<Artist, ArtistDto>(artist)).ToList();

                return artistDtos;
            });
        }

        /// <summary>
        /// Gets an artist by its identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// The retrieved artist.
        /// </returns>
        public async Task<ArtistDto> GetByIdAsync(int id)
        {
            return await Task.Factory.StartNew(() =>
            {
                Artist artist = this.artistRepository.GetById(id);
                ArtistDto artistDto = this.mapper.Map<Artist, ArtistDto>(artist);

                return artistDto;
            });
        }

        #endregion Methods
    }
}