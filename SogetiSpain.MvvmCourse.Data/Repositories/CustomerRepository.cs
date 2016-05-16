// ----------------------------------------------------------------------------
// <copyright file="CustomerRepository.cs" company="SOGETI Spain">
//     Copyright © 2016 SOGETI Spain. All rights reserved.
//     MVVM Course by Óscar Fernández González a.k.a. Osc@rNET.
// </copyright>
// ----------------------------------------------------------------------------
namespace SogetiSpain.MvvmCourse.Data
{
    using Domain;

    /// <summary>
    /// Represents the repository of the customer entity.
    /// </summary>
    public sealed class CustomerRepository : NHibernateRepository<Customer, int>, ICustomerRepository
    {
    }
}