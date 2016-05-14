// ----------------------------------------------------------------------------
// <copyright file="NHibernateRepository.cs" company="SOGETI Spain">
//     Copyright © 2016 SOGETI Spain. All rights reserved.
//     MVVM Course by Óscar Fernández González a.k.a. Osc@rNET.
// </copyright>
// ----------------------------------------------------------------------------
namespace SogetiSpain.MvvmCourse.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Domain;
    using NHibernate;
    using NHibernate.Linq;

    /// <summary>
    /// Represents an abstract repository for NHibernate.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <typeparam name="TId">The type of the identifier.</typeparam>
    public abstract class NHibernateRepository<TEntity, TId> : IRepository<TEntity, TId>
        where TEntity : BaseEntity<TEntity, TId>
        where TId : struct, IEquatable<TId>
    {
        #region Properties

        /// <summary>
        /// Gets the session.
        /// </summary>
        /// <value>
        /// The session.
        /// </value>
        protected ISession Session
        {
            get
            {
                return NHibernateUnitOfWork.Current.Session;
            }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Adds the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void Add(TEntity entity)
        {
            this.Session.Save(entity);
        }

        /// <summary>
        /// Gets all entities.
        /// </summary>
        /// <returns>
        /// The all retrieved entities.
        /// </returns>
        public IEnumerable<TEntity> GetAll()
        {
            return this.Session.Query<TEntity>().ToList();
        }

        /// <summary>
        /// Gets an entity by its identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// The retrieved entity
        /// </returns>
        public TEntity GetById(TId id)
        {
            return this.Session.Get<TEntity>(id);
        }

        /// <summary>
        /// Gets entities by filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="orderByExpression">The order by expression.</param>
        /// <param name="ascending">if set to <c>true</c> [ascending].</param>
        /// <returns>
        /// The retrieved entities.
        /// </returns>
        public IEnumerable<TEntity> GetFiltered(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, object>> orderByExpression = null, bool ascending = true)
        {
            IQueryable<TEntity> query = this.Session.Query<TEntity>().Where(filter);

            if (orderByExpression == null)
            {
                return query.ToList();
            }

            return ascending
                   ? query.OrderBy(orderByExpression).ToList()
                   : query.OrderByDescending(orderByExpression).ToList();
        }

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
        public PagedResult<TEntity> GetPaged(int pageIndex, int pageCount, Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, object>> orderByExpression = null, bool ascending = true)
        {
            IQueryable<TEntity> query = this.Session.Query<TEntity>().Where(filter);

            int total = query.Count();

            return ascending
                   ? new PagedResult<TEntity>(
                       query.OrderBy(orderByExpression)
                            .Skip(pageCount * pageIndex)
                            .Take(pageCount)
                            .ToList(),
                       total)
                   : new PagedResult<TEntity>(
                       query.OrderByDescending(orderByExpression)
                            .Skip(pageCount * pageIndex)
                            .Take(pageCount)
                            .ToList(),
                       total);
        }

        /// <summary>
        /// Modifies the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void Modify(TEntity entity)
        {
            this.Session.Update(entity);
        }

        /// <summary>
        /// Removes the specified entity.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void Remove(int id)
        {
            this.Remove(this.Session.Load<TEntity>(id));
        }

        /// <summary>
        /// Removes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void Remove(TEntity entity)
        {
            this.Session.Delete(entity);
        }

        #endregion Methods
    }
}