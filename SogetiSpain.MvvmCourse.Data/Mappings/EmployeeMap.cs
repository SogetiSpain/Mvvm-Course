// ----------------------------------------------------------------------------
// <copyright file="EmployeeMap.cs" company="SOGETI Spain">
//     Copyright © 2016 SOGETI Spain. All rights reserved.
//     MVVM Course by Óscar Fernández González a.k.a. Osc@rNET.
// </copyright>
// ----------------------------------------------------------------------------
namespace SogetiSpain.MvvmCourse.Data
{
    using Domain;
    using FluentNHibernate.Mapping;

    /// <summary>
    /// Represents the map for the employee entity.
    /// </summary>
    public sealed class EmployeeMap : ClassMap<Employee>
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeeMap"/> class.
        /// </summary>
        public EmployeeMap()
        {
            this.Table("EMPLOYEE");

            this.Id(e => e.Id, "EMPLOYEEID").GeneratedBy.Sequence("SEQ_EMPLOYEE");
            this.Map(e => e.BirthDate, "BIRTHDATE");
            this.Map(e => e.Email, "EMAIL").Length(60);
            this.Map(e => e.Fax, "FAX").Length(24);
            this.Map(e => e.FirstName, "FIRSTNAME").Length(20).Not.Nullable();
            this.Map(e => e.HireDate, "HIREDATE");
            this.Map(e => e.LastName, "LASTNAME").Length(20).Not.Nullable();
            this.Map(e => e.Phone, "PHONE").Length(24);
            this.Map(e => e.Title, "TITLE").Length(30);

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