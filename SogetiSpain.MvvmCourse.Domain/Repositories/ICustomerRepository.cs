// ----------------------------------------------------------------------------
// <copyright file="ICustomerRepository.cs" company="SOGETI Spain">
//     Copyright © 2016 SOGETI Spain. All rights reserved.
//     MVVM Course by Óscar Fernández González a.k.a. Osc@rNET.
// </copyright>
// ----------------------------------------------------------------------------
namespace SogetiSpain.MvvmCourse.Domain
{
    /// <summary>
    /// Defines the contract for a repository of the customer entity.
    /// </summary>
    public interface ICustomerRepository : IRepository<Customer, int>
    {
    }
}