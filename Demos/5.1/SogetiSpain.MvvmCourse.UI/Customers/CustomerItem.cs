// ----------------------------------------------------------------------------
// <copyright file="CustomerItem.cs" company="SOGETI Spain">
//     Copyright © 2016 SOGETI Spain. All rights reserved.
//     MVVM Course by Óscar Fernández González a.k.a. Osc@rNET.
// </copyright>
// ----------------------------------------------------------------------------
namespace SogetiSpain.MvvmCourse.UI.Customers
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Represents a customer item.
    /// </summary>
    public sealed class CustomerItem : ValidatableBindableBase
    {
        #region Fields

        /// <summary>
        /// Defines the address.
        /// </summary>
        private string address;

        /// <summary>
        /// Defines the company.
        /// </summary>
        private string company;

        /// <summary>
        /// Defines the city.
        /// </summary>
        private string city;

        /// <summary>
        /// Defines the country.
        /// </summary>
        private string country;

        /// <summary>
        /// Defines the email.
        /// </summary>
        private string email;

        /// <summary>
        /// Defines the fax.
        /// </summary>
        private string fax;

        /// <summary>
        /// Defines the first name.
        /// </summary>
        private string firstName;

        /// <summary>
        /// Defines the identifier.
        /// </summary>
        private int id;

        /// <summary>
        /// Defines the last name.
        /// </summary>
        private string lastName;

        /// <summary>
        /// Defines the phone.
        /// </summary>
        private string phone;

        /// <summary>
        /// Defines the postal code.
        /// </summary>
        private string postalCode;

        /// <summary>
        /// Defines the state.
        /// </summary>
        private string state;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        /// <value>
        /// The address.
        /// </value>
        [StringLength(70)]
        public string Address
        {
            get
            {
                return this.address;
            }

            set
            {
                this.SetProperty(ref this.address, value);
            }
        }

        /// <summary>
        /// Gets or sets the company.
        /// </summary>
        /// <value>
        /// The company.
        /// </value>
        [StringLength(80)]
        public string Company
        {
            get
            {
                return this.company;
            }

            set
            {
                this.SetProperty(ref this.company, value);
            }
        }

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        /// <value>
        /// The city.
        /// </value>
        [StringLength(40)]
        public string City
        {
            get
            {
                return this.city;
            }

            set
            {
                this.SetProperty(ref this.city, value);
            }
        }

        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        /// <value>
        /// The country.
        /// </value>
        [StringLength(40)]
        public string Country
        {
            get
            {
                return this.country;
            }

            set
            {
                this.SetProperty(ref this.country, value);
            }
        }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        [EmailAddress]
        [Required]
        [StringLength(60)]
        public string Email
        {
            get
            {
                return this.email;
            }

            set
            {
                this.SetProperty(ref this.email, value);
            }
        }

        /// <summary>
        /// Gets or sets the fax.
        /// </summary>
        /// <value>
        /// The fax.
        /// </value>
        [Phone]
        [StringLength(24)]
        public string Fax
        {
            get
            {
                return this.fax;
            }

            set
            {
                this.SetProperty(ref this.fax, value);
            }
        }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        [Required]
        [StringLength(40)]
        public string FirstName
        {
            get
            {
                return this.firstName;
            }

            set
            {
                this.SetProperty(ref this.firstName, value);
            }
        }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id
        {
            get
            {
                return this.id;
            }

            set
            {
                this.SetProperty(ref this.id, value);
            }
        }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        [Required]
        [StringLength(20)]
        public string LastName
        {
            get
            {
                return this.lastName;
            }

            set
            {
                this.SetProperty(ref this.lastName, value);
            }
        }

        /// <summary>
        /// Gets or sets the phone.
        /// </summary>
        /// <value>
        /// The phone.
        /// </value>
        [Phone]
        [StringLength(24)]
        public string Phone
        {
            get
            {
                return this.phone;
            }

            set
            {
                this.SetProperty(ref this.phone, value);
            }
        }

        /// <summary>
        /// Gets or sets the postal code.
        /// </summary>
        /// <value>
        /// The postal code.
        /// </value>
        [StringLength(10)]
        public string PostalCode
        {
            get
            {
                return this.postalCode;
            }

            set
            {
                this.SetProperty(ref this.postalCode, value);
            }
        }

        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        /// <value>
        /// The state.
        /// </value>
        [StringLength(40)]
        public string State
        {
            get
            {
                return this.state;
            }

            set
            {
                this.SetProperty(ref this.state, value);
            }
        }

        #endregion Properties
    }
}