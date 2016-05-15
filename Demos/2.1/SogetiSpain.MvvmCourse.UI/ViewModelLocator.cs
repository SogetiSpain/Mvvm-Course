// ----------------------------------------------------------------------------
// <copyright file="ViewModelLocator.cs" company="SOGETI Spain">
//     Copyright © 2016 SOGETI Spain. All rights reserved.
//     MVVM Course by Óscar Fernández González a.k.a. Osc@rNET.
// </copyright>
// ----------------------------------------------------------------------------
namespace SogetiSpain.MvvmCourse.UI
{
    using System;
    using System.ComponentModel;
    using System.Windows;

    /// <summary>
    /// Represents the view model locator.
    /// </summary>
    public static class ViewModelLocator
    {
        #region Fields

        /// <summary>
        /// Defines the 'AutoWireViewModel' property.
        /// </summary>
        private static readonly DependencyProperty AutoWireViewModelProperty =
            DependencyProperty.RegisterAttached(
                "AutoWireViewModel",
                typeof(bool),
                typeof(ViewModelLocator),
                new PropertyMetadata(false, ViewModelLocator.AutoWireViewModelChanged));

        #endregion Fields

        #region Methods

        /// <summary>
        /// Gets a value indicating whether it automatically performs locating and hooking up the view model.
        /// </summary>
        /// <param name="view">The view.</param>
        /// <returns>
        ///   <c>true</c> if it automatically performs locating and hooking up the view model; otherwise, <c>false</c>.
        /// </returns>
        public static bool GetAutoWireViewModel(DependencyObject view)
        {
            return (bool)view.GetValue(ViewModelLocator.AutoWireViewModelProperty);
        }

        /// <summary>
        /// Sets a value indicating whether it automatically performs locating and hooking up the view model.
        /// </summary>
        /// <param name="view">The view.</param>
        /// <param name="value"><c>true</c> if it automatically performs locating and hooking up the view model; otherwise, <c>false</c>.</param>
        public static void SetAutoWireViewModel(DependencyObject view, bool value)
        {
            view.SetValue(ViewModelLocator.AutoWireViewModelProperty, value);
        }

        /// <summary>
        /// Called when the 'AutoWireViewModel' property is changed.
        /// </summary>
        /// <param name="view">The view.</param>
        /// <param name="e">The <see cref="DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void AutoWireViewModelChanged(
            DependencyObject view,
            DependencyPropertyChangedEventArgs e)
        {
            if (DesignerProperties.GetIsInDesignMode(view))
            {
                return;
            }

            // What View is being constructed?
            Type viewType = view.GetType();
            string viewTypeName = viewType.FullName;

            // What ViewModel should I construct?
            string viewModelTypeName = viewTypeName + "Model";

            // Construct ViewModel
            Type viewModelType = Type.GetType(viewModelTypeName);
            object viewModel = Activator.CreateInstance(viewModelType);

            // Set DataContext
            ((FrameworkElement)view).DataContext = viewModel;
        }

        #endregion Methods
    }
}