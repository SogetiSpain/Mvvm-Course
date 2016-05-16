// ----------------------------------------------------------------------------
// <copyright file="ICustomerServiceClient.cs" company="SOGETI Spain">
//     Copyright © 2016 SOGETI Spain. All rights reserved.
//     MVVM Course by Óscar Fernández González a.k.a. Osc@rNET.
// </copyright>
// ----------------------------------------------------------------------------
namespace SogetiSpain.MvvmCourse.UI.Services
{
    using SogetiSpain.MvvmCourse.WebServices;

    /// <summary>
    /// Defines the contract for the customer service client.
    /// </summary>
    public interface ICustomerServiceClient : IServiceClient<ICustomerService>
    {
    }
}
