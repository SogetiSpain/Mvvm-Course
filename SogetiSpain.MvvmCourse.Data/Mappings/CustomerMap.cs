// ----------------------------------------------------------------------------
// <copyright file="CustomerMap.cs" company="SOGETI Spain">
//     Copyright © 2016 SOGETI Spain. All rights reserved.
//     MVVM Course by Óscar Fernández González a.k.a. Osc@rNET.
// </copyright>
// ----------------------------------------------------------------------------
namespace SogetiSpain.MvvmCourse.Data
{
    using Domain;
    using FluentNHibernate.Mapping;

    /// <summary>
    /// Represents the map for the customer entity.
    /// </summary>
    public sealed class CustomerMap : ClassMap<Customer>
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerMap"/> class.
        /// </summary>
        public CustomerMap()
        {
            this.Table("CUSTOMER");

            this.Id(e => e.Id, "CUSTOMERID").GeneratedBy.Sequence("SEQ_CUSTOMER");
            this.Map(e => e.Company, "COMPANY").Length(80);
            this.Map(e => e.Email, "EMAIL").Length(60).Not.Nullable();
            this.Map(e => e.Fax, "FAX").Length(24);
            this.Map(e => e.FirstName, "FIRSTNAME").Length(40).Not.Nullable();
            this.Map(e => e.LastName, "LASTNAME").Length(20).Not.Nullable();
            this.Map(e => e.Phone, "PHONE").Length(24);

            this.Component<Address>(
                e => e.Address,
                m =>
                {
                    m.Map(e => e.AddressLine1, "ADDRESS").Length(70);
                    m.Map(e => e.City, "CITY").Length(40);
                    m.Map(e => e.Country, "COUNTRY").Length(40);
                    m.Map(e => e.PostalCode, "POSTALCODE").Length(10);
                    m.Map(e => e.State, "STATE").Length(40);
                });

            this.DynamicInsert();
            this.DynamicUpdate();
        }

        #endregion Constructors
    }
}