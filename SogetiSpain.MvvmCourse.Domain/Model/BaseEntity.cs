// ----------------------------------------------------------------------------
// <copyright file="BaseEntity.cs" company="SOGETI Spain">
//     Copyright © 2016 SOGETI Spain. All rights reserved.
//     MVVM Course by Óscar Fernández González a.k.a. Osc@rNET.
// </copyright>
// ----------------------------------------------------------------------------
namespace SogetiSpain.MvvmCourse.Domain
{
    using System;

    /// <summary>
    /// Represents an abstract entity.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <typeparam name="TId">The type of the identifier.</typeparam>
    public abstract class BaseEntity<TEntity, TId> : IEquatable<TEntity>
        where TEntity : BaseEntity<TEntity, TId>
        where TId : struct, IEquatable<TId>
    {
        #region Fields

        /// <summary>
        /// Defines the cached hash code.
        /// </summary>
        private int? cachedHashCode;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseEntity{TEntity, TId}"/> class.
        /// </summary>
        protected BaseEntity()
        {
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public virtual TId Id
        {
            get;
            protected set;
        }

        #endregion Properties

        #region Operators

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="operand1">The first operand.</param>
        /// <param name="operand2">The second operand.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator !=(BaseEntity<TEntity, TId> operand1, BaseEntity<TEntity, TId> operand2)
        {
            return !(operand1 == operand2);
        }

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="operand1">The first operand.</param>
        /// <param name="operand2">The second operand.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator ==(BaseEntity<TEntity, TId> operand1, BaseEntity<TEntity, TId> operand2)
        {
            return object.Equals(operand1, operand2);
        }

        #endregion Operators

        #region Methods

        /// <summary>
        /// Determines whether the specified instance, is equal to this instance.
        /// </summary>
        /// <param name="other">The instance to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified instance is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public virtual bool Equals(TEntity other)
        {
            return base.Equals(other);
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            var other = obj as BaseEntity<TEntity, TId>;

            if (object.ReferenceEquals(this, other))
            {
                return true;
            }

            if ((other == null) || !(other is TEntity))
            {
                return false;
            }

            if (this.IsTransient())
            {
                return false;
            }

            return this.HasSameNonDefaultIdAs(other);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            if (this.cachedHashCode.HasValue)
            {
                return this.cachedHashCode.Value;
            }

            if (this.IsTransient())
            {
                this.cachedHashCode = base.GetHashCode();
            }
            else
            {
                unchecked
                {
                    int hashCode = this.GetType().GetHashCode();
                    this.cachedHashCode = (hashCode * 33) ^ this.Id.GetHashCode();
                }
            }

            return this.cachedHashCode.Value;
        }

        /// <summary>
        /// Determines whether this instance is transient.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance is transient; otherwise, <c>false</c>.
        /// </returns>
        public virtual bool IsTransient()
        {
            return this.Id.Equals(default(TId));
        }

        /// <summary>
        /// Determines whether this instance has the same non default identifier as the specified instance.
        /// </summary>
        /// <param name="other">The instance to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if this instance has the same non default identifier; otherwise, <c>false</c>.
        /// </returns>
        private bool HasSameNonDefaultIdAs(BaseEntity<TEntity, TId> other)
        {
            return !this.IsTransient() && !other.IsTransient() && this.Id.Equals(other.Id);
        }

        #endregion Methods
    }
}