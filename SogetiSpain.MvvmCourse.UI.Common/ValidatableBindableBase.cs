// ----------------------------------------------------------------------------
// <copyright file="ValidatableBindableBase.cs" company="SOGETI Spain">
//     Copyright © 2016 SOGETI Spain. All rights reserved.
//     MVVM Course by Óscar Fernández González a.k.a. Osc@rNET.
// </copyright>
// ----------------------------------------------------------------------------
namespace SogetiSpain.MvvmCourse.UI
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Runtime.CompilerServices;

    /// <summary>
    /// Implementation of <see cref="INotifyPropertyChanged"/> to simplify models that supports validation.
    /// </summary>
    public abstract class ValidatableBindableBase : BindableBase, INotifyDataErrorInfo
    {
        #region Fields

        /// <summary>
        /// Defines the errors dictionary.
        /// </summary>
        private Dictionary<string, List<string>> errors = new Dictionary<string, List<string>>();

        #endregion Fields

        #region Events

        /// <summary>
        /// Occurs when the validation errors have changed for a property or for the entire entity.
        /// </summary>
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged = delegate { };

        #endregion Events

        #region Properties

        /// <summary>
        /// Gets a value indicating whether the entity has validation errors.
        /// </summary>
        public bool HasErrors
        {
            get
            {
                return this.errors.Count > 0;
            }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Gets the validation errors for a specified property or for the entire entity.
        /// </summary>
        /// <param name="propertyName">The name of the property to retrieve validation errors for; or null or <see cref="F:System.String.Empty" />, to retrieve entity-level errors.</param>
        /// <returns>
        /// The validation errors for the property or entity.
        /// </returns>
        public IEnumerable GetErrors(string propertyName)
        {
            if (this.errors.ContainsKey(propertyName))
            {
                return this.errors[propertyName];
            }

            return null;
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
        protected override bool SetProperty<TProperty>(
            ref TProperty storage,
            TProperty value,
            [CallerMemberName] string propertyName = null)
        {
            bool wasChanged = base.SetProperty<TProperty>(ref storage, value, propertyName);
            this.ValidateProperty(propertyName, value);

            return wasChanged;
        }

        /// <summary>
        /// Validates the specified property.
        /// </summary>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="value">The value.</param>
        private void ValidateProperty<TProperty>(string propertyName, TProperty value)
        {
            List<ValidationResult> results = new List<ValidationResult>();
            ValidationContext context = new ValidationContext(this);
            context.MemberName = propertyName;
            Validator.TryValidateProperty(value, context, results);

            if (results.Any())
            {
                this.errors[propertyName] = results.Select(c => c.ErrorMessage).ToList();
            }
            else
            {
                this.errors.Remove(propertyName);
            }

            this.ErrorsChanged(this, new DataErrorsChangedEventArgs(propertyName));
        }

        #endregion Methods
    }
}