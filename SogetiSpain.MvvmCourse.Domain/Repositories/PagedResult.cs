// ----------------------------------------------------------------------------
// <copyright file="PagedResult.cs" company="SOGETI Spain">
//     Copyright © 2016 SOGETI Spain. All rights reserved.
//     MVVM Course by Óscar Fernández González a.k.a. Osc@rNET.
// </copyright>
// ----------------------------------------------------------------------------
namespace SogetiSpain.MvvmCourse.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    /// <summary>
    /// Represents a paged result.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    [DataContract]
    public class PagedResult<TEntity>
        where TEntity : class
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PagedResult{TEntity}"/> class.
        /// </summary>
        /// <param name="items">The items.</param>
        /// <param name="totalItems">The total items.</param>
        public PagedResult(IEnumerable<TEntity> items, int totalItems)
        {
            this.Items = items;
            this.TotalItems = totalItems;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the items.
        /// </summary>
        /// <value>
        /// The items.
        /// </value>
        [DataMember]
        public IEnumerable<TEntity> Items
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the total items.
        /// </summary>
        /// <value>
        /// The total items.
        /// </value>
        [DataMember]
        public int TotalItems
        {
            get;
            private set;
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Gets the total pages.
        /// </summary>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>
        /// The total pages.
        /// </returns>
        public int TotalPages(int pageSize)
        {
            return (int)Math.Ceiling(Convert.ToDouble(this.TotalItems) / pageSize);
        }

        #endregion Methods
    }
}