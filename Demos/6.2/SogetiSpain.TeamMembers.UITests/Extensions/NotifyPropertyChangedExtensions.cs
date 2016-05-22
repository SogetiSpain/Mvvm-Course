// ----------------------------------------------------------------------------
// <copyright file="NotifyPropertyChangedExtensions.cs" company="SOGETI Spain">
//     Copyright © 2016 SOGETI Spain. All rights reserved.
//     MVVM Course by Óscar Fernández González a.k.a. Osc@rNET.
// </copyright>
// ----------------------------------------------------------------------------
namespace SogetiSpain.TeamMembers.UITests.Extensions
{
    using System;
    using System.ComponentModel;

    /// <summary>
    /// Defines the extensions methods for <see cref="INotifyPropertyChanged"/>.
    /// </summary>
    public static class NotifyPropertyChangedExtensions
    {
        #region Methods

        /// <summary>
        /// Determines whether the property changed is fired.
        /// </summary>
        /// <param name="notifyPropertyChanged">The notify property changed.</param>
        /// <param name="action">The action.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns>
        ///   <c>true</c> if the property changed is fired; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsPropertyChangedFired(
            this INotifyPropertyChanged notifyPropertyChanged,
            Action action,
            string propertyName)
        {
            bool fired = false;
            notifyPropertyChanged.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == propertyName)
                {
                    fired = true;
                }
            };

            action();

            return fired;
        }

        #endregion Methods
    }
}