// ----------------------------------------------------------------------------
// <copyright file="Address.cs" company="SOGETI Spain">
//     Copyright © 2016 SOGETI Spain. All rights reserved.
//     MVVM Course by Óscar Fernández González a.k.a. Osc@rNET.
// </copyright>
// ----------------------------------------------------------------------------
namespace SogetiSpain.MvvmCourse.Domain
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Represents an address value object.
    /// </summary>
    public class Address : ValueObject<Address>
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Address"/> class.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <param name="city">The city.</param>
        /// <param name="state">The state.</param>
        /// <param name="country">The country.</param>
        /// <param name="postalcode">The postal code.</param>
        public Address(
            string address,
            string city,
            string state,
            string country,
            string postalcode)
        {
            this.AddressLine1 = address;
            this.City = city;
            this.State = state;
            this.Country = country;
            this.PostalCode = postalcode;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Address"/> class.
        /// </summary>
        protected Address()
        {
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets the address line1.
        /// </summary>
        /// <value>
        /// The address line1.
        /// </value>
        [StringLength(70)]
        public virtual string AddressLine1
        {
            get;
            protected set;
        }

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        /// <value>
        /// The city.
        /// </value>
        [StringLength(40)]
        public virtual string City
        {
            get;
            protected set;
        }

        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        /// <value>
        /// The country.
        /// </value>
        [StringLength(40)]
        public virtual string Country
        {
            get;
            protected set;
        }

        /// <summary>
        /// Gets or sets the postal code.
        /// </summary>
        /// <value>
        /// The postal code.
        /// </value>
        [StringLength(10)]
        public virtual string PostalCode
        {
            get;
            protected set;
        }

        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        /// <value>
        /// The state.
        /// </value>
        [StringLength(40)]
        public virtual string State
        {
            get;
            protected set;
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Gets the attributes for equality check.
        /// </summary>
        /// <returns>
        /// A sequence of the attributes for equality check.
        /// </returns>
        protected override IEnumerable<object> GetAttributesForEqualityCheck()
        {
            return new object[] { this.AddressLine1, this.City, this.Country, this.PostalCode, this.State };
        }

        #endregion Methods
    }
}