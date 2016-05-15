// ----------------------------------------------------------------------------
// <copyright file="ExampleAttachedBehavior.cs" company="SOGETI Spain">
//     Copyright © 2016 SOGETI Spain. All rights reserved.
//     MVVM Course by Óscar Fernández González a.k.a. Osc@rNET.
// </copyright>
// ----------------------------------------------------------------------------
namespace SogetiSpain.MvvmCourse.UI
{
    using System.Reflection;
    using System.Windows;

    /// <summary>
    /// Represents the attached behavior for execute a method when the view is loaded.
    /// </summary>
    public static class ExampleAttachedBehavior
    {
        #region Fields

        /// <summary>
        /// Defines the 'MethodNameToExecute' property.
        /// </summary>
        private static readonly DependencyProperty MethodNameToExecuteProperty =
            DependencyProperty.RegisterAttached(
                "MethodNameToExecute",
                typeof(string),
                typeof(ExampleAttachedBehavior),
                new PropertyMetadata(null, ExampleAttachedBehavior.OnMethodNameToExecuteChanged));

        #endregion Fields

        #region Methods

        /// <summary>
        /// Gets the method name to execute when the view is loaded.
        /// </summary>
        /// <param name="dependencyObject">The dependency object.</param>
        /// <returns>
        /// The method name to execute when the view is loaded.
        /// </returns>
        public static string GetMethodNameToExecute(DependencyObject dependencyObject)
        {
            return (string)dependencyObject.GetValue(ExampleAttachedBehavior.MethodNameToExecuteProperty);
        }

        /// <summary>
        /// Sets the method name to execute when the view is loaded.
        /// </summary>
        /// <param name="dependencyObject">The dependency object.</param>
        /// <param name="value">The method name to execute when the view is loaded.</param>
        public static void SetMethodNameToExecute(DependencyObject dependencyObject, string value)
        {
            dependencyObject.SetValue(ExampleAttachedBehavior.MethodNameToExecuteProperty, value);
        }

        /// <summary>
        /// Called when the 'MethodNameToExecute' property is changed.
        /// </summary>
        /// <param name="dependencyObject">The dependency object.</param>
        /// <param name="e">The <see cref="DependencyPropertyChangedEventArgs" /> instance containing the event data.</param>
        private static void OnMethodNameToExecuteChanged(
            DependencyObject dependencyObject,
            DependencyPropertyChangedEventArgs e)
        {
            FrameworkElement view = dependencyObject as FrameworkElement;
            if (view != null)
            {
                view.Loaded += (sender, args) =>
                {
                    object viewModel = view.DataContext;
                    if (viewModel == null)
                    {
                        return;
                    }

                    MethodInfo methodInfo = viewModel.GetType().GetMethod(e.NewValue.ToString());
                    if (methodInfo != null)
                    {
                        methodInfo.Invoke(viewModel, null);
                    }
                };
            }
        }

        #endregion Methods
    }
}