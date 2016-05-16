// ----------------------------------------------------------------------------
// <copyright file="NegatableBooleanToVisibilityConverter.cs" company="SOGETI Spain">
//     Copyright © 2016 SOGETI Spain. All rights reserved.
//     MVVM Course by Óscar Fernández González a.k.a. Osc@rNET.
// </copyright>
// ----------------------------------------------------------------------------
namespace SogetiSpain.MvvmCourse.UI.Converters
{
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;

    /// <summary>
    /// Represents the converter from boolean to visibility (also allows the negation of boolean).
    /// </summary>
    public class NegatableBooleanToVisibilityConverter : IValueConverter
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="NegatableBooleanToVisibilityConverter"/> class.
        /// </summary>
        public NegatableBooleanToVisibilityConverter()
        {
            this.FalseVisibility = Visibility.Collapsed;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="NegatableBooleanToVisibilityConverter"/> is negate.
        /// </summary>
        /// <value>
        ///   <c>true</c> if negate; otherwise, <c>false</c>.
        /// </value>
        public bool Negate
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the false visibility.
        /// </summary>
        /// <value>
        /// The false visibility.
        /// </value>
        public Visibility FalseVisibility
        {
            get;
            set;
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Converts a value.
        /// </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>
        /// A converted value. If the method returns null, the valid null value is used.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool parsedValue = false;
            bool result = bool.TryParse(value.ToString(), out parsedValue);

            if (!result)
            {
                return value;
            }

            if (parsedValue && !this.Negate)
            {
                return Visibility.Visible;
            }

            if (parsedValue && this.Negate)
            {
                return this.FalseVisibility;
            }

            if (!parsedValue && this.Negate)
            {
                return Visibility.Visible;
            }

            if (!parsedValue && !this.Negate)
            {
                return this.FalseVisibility;
            }
            else
            {
                return Visibility.Visible;
            }
        }

        /// <summary>
        /// Converts a value.
        /// </summary>
        /// <param name="value">The value that is produced by the binding target.</param>
        /// <param name="targetType">The type to convert to.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>
        /// A converted value. If the method returns null, the valid null value is used.
        /// </returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }

        #endregion Methods
    }
}