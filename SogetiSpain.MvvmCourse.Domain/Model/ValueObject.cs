// ----------------------------------------------------------------------------
// <copyright file="ValueObject.cs" company="SOGETI Spain">
//     Copyright © 2016 SOGETI Spain. All rights reserved.
//     MVVM Course by Óscar Fernández González a.k.a. Osc@rNET.
// </copyright>
// ----------------------------------------------------------------------------
namespace SogetiSpain.MvvmCourse.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents an abstract value object.
    /// </summary>
    /// <typeparam name="T">The type of value object.</typeparam>
    public abstract class ValueObject<T> : IEquatable<T>
        where T : ValueObject<T>
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ValueObject{T}"/> class.
        /// </summary>
        protected ValueObject()
        {
        }

        #endregion Constructors

        #region Operators

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="operand1">The first operand.</param>
        /// <param name="operand2">The second operand.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator !=(ValueObject<T> operand1, ValueObject<T> operand2)
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
        public static bool operator ==(ValueObject<T> operand1, ValueObject<T> operand2)
        {
            return object.Equals(operand1, operand2);
        }

        #endregion Operators

        #region Methods

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            return this.Equals(obj as T);
        }

        /// <summary>
        /// Determines whether the specified instance, is equal to this instance.
        /// </summary>
        /// <param name="other">The instance to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified instance is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public virtual bool Equals(T other)
        {
            if (other == null)
            {
                return false;
            }

            return this.GetAttributesForEqualityCheck()
                .SequenceEqual(other.GetAttributesForEqualityCheck());
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            int hash = this.GetType().GetHashCode();

            foreach (var attribute in this.GetAttributesForEqualityCheck())
            {
                unchecked
                {
                    hash *= 33 + (attribute == null ? 0 : attribute.GetHashCode());
                }
            }

            return hash;
        }

        /// <summary>
        /// Gets the attributes for equality check.
        /// </summary>
        /// <returns>
        /// A sequence of the attributes for equality check.
        /// </returns>
        protected abstract IEnumerable<object> GetAttributesForEqualityCheck();

        #endregion Methods
    }
}