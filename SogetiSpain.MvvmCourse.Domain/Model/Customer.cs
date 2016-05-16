// ----------------------------------------------------------------------------
// <copyright file="Customer.cs" company="SOGETI Spain">
//     Copyright © 2016 SOGETI Spain. All rights reserved.
//     MVVM Course by Óscar Fernández González a.k.a. Osc@rNET.
// </copyright>
// ----------------------------------------------------------------------------
namespace SogetiSpain.MvvmCourse.Domain
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Represents a customer entity.
    /// </summary>
    public class Customer : BaseEntity<Customer, int>
    {
        #region Properties

        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        /// <value>
        /// The address.
        /// </value>
        [Required]
        public virtual Address Address
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
        [StringLength(80)]
        public virtual string Company
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
        [Required]
        [StringLength(60)]
        public virtual string Email
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
        [StringLength(24)]
        public virtual string Fax
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
        [Required]
        [StringLength(40)]
        public virtual string FirstName
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
        [Required]
        [StringLength(20)]
        public virtual string LastName
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
        [StringLength(24)]
        public virtual string Phone
        {
            get;
            set;
        }

        #endregion Properties
    }
}