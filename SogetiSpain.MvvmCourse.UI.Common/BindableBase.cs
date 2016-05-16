// ----------------------------------------------------------------------------
// <copyright file="BindableBase.cs" company="SOGETI Spain">
//     Copyright © 2016 SOGETI Spain. All rights reserved.
//     MVVM Course by Óscar Fernández González a.k.a. Osc@rNET.
// </copyright>
// ----------------------------------------------------------------------------
namespace SogetiSpain.MvvmCourse.UI
{
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    /// <summary>
    /// Implementation of <see cref="INotifyPropertyChanged"/> to simplify models.
    /// </summary>
    public abstract class BindableBase : INotifyPropertyChanged
    {
        #region Events

        /// <summary>
        /// Notifies clients that a property value has changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Events

        #region Methods

        /// <summary>
        /// Notifies listeners that a property value has changed.
        /// </summary>
        /// <param name="propertyName">
        /// Name of the property used to notify listeners. This value is optional and can be
        /// provided automatically when invoked from compilers that support <see cref="CallerMemberNameAttribute"/>.
        /// </param>
        protected virtual void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Checks if a property already matches a desired value.
        /// Sets the property and notifies listeners only when necessary.
        /// </summary>
        /// <typeparam name="TProperty">Type of the property.</typeparam>
        /// <param name="storage">Reference to a property with both getter and setter.</param>
        /// <param name="value">Desired value for the property.</param>
        /// <param name="propertyName">
        /// Name of the property used to notify listeners. This value is optional and can be
        /// provided automatically when invoked from compilers that support <see cref="CallerMemberNameAttribute"/>.
        /// </param>
        /// <returns>
        ///   <c>true</c> if the value was changed;
        ///   otherwise, <c>false</c> if the existing value matched the desired value.
        /// </returns>
        protected virtual bool SetProperty<TProperty>(
            ref TProperty storage,
            TProperty value,
            [CallerMemberName] string propertyName = null)
        {
            if (object.Equals(storage, value))
            {
                return false;
            }

            storage = value;
            this.OnPropertyChanged(propertyName);

            return true;
        }

        #endregion Methods
    }
}