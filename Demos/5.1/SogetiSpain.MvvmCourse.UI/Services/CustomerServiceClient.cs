// ----------------------------------------------------------------------------
// <copyright file="CustomerServiceClient.cs" company="SOGETI Spain">
//     Copyright © 2016 SOGETI Spain. All rights reserved.
//     MVVM Course by Óscar Fernández González a.k.a. Osc@rNET.
// </copyright>
// ----------------------------------------------------------------------------
namespace SogetiSpain.MvvmCourse.UI.Services
{
    using SogetiSpain.MvvmCourse.WebServices;

    /// <summary>
    /// Represents the customer service client.
    /// </summary>
    public sealed class CustomerServiceClient : ServiceClient<ICustomerService>, ICustomerServiceClient
    {
    }
}