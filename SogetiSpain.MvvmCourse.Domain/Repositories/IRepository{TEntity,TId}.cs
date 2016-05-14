// ----------------------------------------------------------------------------
// <copyright file="IRepository{TEntity,TId}.cs" company="SOGETI Spain">
//     Copyright © 2016 SOGETI Spain. All rights reserved.
//     MVVM Course by Óscar Fernández González a.k.a. Osc@rNET.
// </copyright>
// ----------------------------------------------------------------------------
namespace SogetiSpain.MvvmCourse.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// Defines the contract for a repository.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <typeparam name="TId">The type of the identifier.</typeparam>
    public interface IRepository<TEntity, TId> : IRepository
        where TEntity : BaseEntity<TEntity, TId>
        where TId : struct, IEquatable<TId>
    {
        #region Methods

        /// <summary>
        /// Adds the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Add(TEntity entity);

        /// <summary>
        /// Gets all entities.
        /// </summary>
        /// <returns>
        /// The all retrieved entities.
        /// </returns>
        IEnumerable<TEntity> GetAll();

        /// <summary>
        /// Gets an entity by its identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// The retrieved entity
        /// </returns>
        TEntity GetById(TId id);

        /// <summary>
        /// Gets entities by filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="orderByExpression">The order by expression.</param>
        /// <param name="ascending">if set to <c>true</c> [ascending].</param>
        /// <returns>
        /// The retrieved entities.
        /// </returns>
        IEnumerable<TEntity> GetFiltered(
            Expression<Func<TEntity, bool>> filter,
            Expression<Func<TEntity, object>> orderByExpression = null,
            bool ascending = true);

        /// <summary>
        /// Gets paged entities.
        /// </summary>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageCount">The page count.</param>
        /// <param name="filter">The filter.</param>
        /// <param name="orderByExpression">The order by expression.</param>
        /// <param name="ascending">if set to <c>true</c> ascending.</param>
        /// <returns>
        /// The paged entities.
        /// </returns>
        PagedResult<TEntity> GetPaged(
            int pageIndex,
            int pageCount,
            Expression<Func<TEntity, bool>> filter,
            Expression<Func<TEntity, object>> orderByExpression = null,
            bool ascending = true);

        /// <summary>
        /// Modifies the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Modify(TEntity entity);

        /// <summary>
        /// Removes the specified entity.
        /// </summary>
        /// <param name="id">The identifier.</param>
        void Remove(int id);

        /// <summary>
        /// Removes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Remove(TEntity entity);

        #endregion Methods
    }
}