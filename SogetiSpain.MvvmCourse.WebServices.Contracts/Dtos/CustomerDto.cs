// ----------------------------------------------------------------------------
// <copyright file="CustomerDto.cs" company="SOGETI Spain">
//     Copyright © 2016 SOGETI Spain. All rights reserved.
//     MVVM Course by Óscar Fernández González a.k.a. Osc@rNET.
// </copyright>
// ----------------------------------------------------------------------------
namespace SogetiSpain.MvvmCourse.WebServices
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Represents a customer data transfer object.
    /// </summary>
    [DataContract]
    public class CustomerDto
    {
        #region Properties

        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        /// <value>
        /// The address.
        /// </value>
        [DataMember]
        public string Address
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the company.
        /// </summary>
        /// <value>
        /// The company.
        /// </value>
        [DataMember]
        public string Company
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        /// <value>
        /// The city.
        /// </value>
        [DataMember]
        public string City
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        /// <value>
        /// The country.
        /// </value>
        [DataMember]
        public string Country
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        [DataMember]
        public string Email
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the fax.
        /// </summary>
        /// <value>
        /// The fax.
        /// </value>
        [DataMember]
        public string Fax
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        [DataMember]
        public string FirstName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [DataMember]
        public int Id
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        [DataMember]
        public string LastName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the phone.
        /// </summary>
        /// <value>
        /// The phone.
        /// </value>
        [DataMember]
        public string Phone
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the postal code.
        /// </summary>
        /// <value>
        /// The postal code.
        /// </value>
        [DataMember]
        public string PostalCode
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        /// <value>
        /// The state.
        /// </value>
        [DataMember]
        public string State
        {
            get;
            set;
        }

        #endregion Properties
    }
}