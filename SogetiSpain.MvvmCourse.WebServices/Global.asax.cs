// ----------------------------------------------------------------------------
// <copyright file="Global.asax.cs" company="SOGETI Spain">
//     Copyright © 2016 SOGETI Spain. All rights reserved.
//     MVVM Course by Óscar Fernández González a.k.a. Osc@rNET.
// </copyright>
// ----------------------------------------------------------------------------
namespace SogetiSpain.MvvmCourse.WebServices
{
    using System;
    using System.Web;

    /// <summary>
    /// Represents the ASP.NET application.
    /// </summary>
    public class Global : HttpApplication
    {
        #region Methods

        /// <summary>
        /// Handles the Start event of the Application control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Application_Start(object sender, EventArgs e)
        {
            Bootstrapper.Run();
        }

        #endregion Methods
    }
}