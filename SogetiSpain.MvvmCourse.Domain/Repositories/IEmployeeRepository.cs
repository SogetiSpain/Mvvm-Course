// ----------------------------------------------------------------------------
// <copyright file="IEmployeeRepository.cs" company="SOGETI Spain">
//     Copyright © 2016 SOGETI Spain. All rights reserved.
//     MVVM Course by Óscar Fernández González a.k.a. Osc@rNET.
// </copyright>
// ----------------------------------------------------------------------------
namespace SogetiSpain.MvvmCourse.Domain
{
    /// <summary>
    /// Defines the contract for a repository of the employee entity.
    /// </summary>
    public interface IEmployeeRepository : IRepository<Employee, int>
    {
    }
}