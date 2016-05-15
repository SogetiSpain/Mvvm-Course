// ----------------------------------------------------------------------------
// <copyright file="ExecuteMethodOnLoadedBehaviour.cs" company="SOGETI Spain">
//     Copyright © 2016 SOGETI Spain. All rights reserved.
//     MVVM Course by Óscar Fernández González a.k.a. Osc@rNET.
// </copyright>
// ----------------------------------------------------------------------------
namespace SogetiSpain.MvvmCourse.UI
{
    using System.Reflection;
    using System.Windows;

    /// <summary>
    /// Represents the behaviour for execute a method when the view is loaded.
    /// </summary>
    public static class ExecuteMethodOnLoadedBehaviour
    {
        #region Fields

        /// <summary>
        /// Defines the 'MethodNameToExecute' property.
        /// </summary>
        private static readonly DependencyProperty MethodNameToExecuteProperty =
            DependencyProperty.RegisterAttached(
                "MethodNameToExecute",
                typeof(string),
                typeof(ExecuteMethodOnLoadedBehaviour),
                new PropertyMetadata(null, ExecuteMethodOnLoadedBehaviour.MethodNameToExecuteChanged));

        #endregion Fields

        #region Methods

        /// <summary>
        /// Gets the method name to execute when the view is loaded.
        /// </summary>
        /// <param name="view">The view.</param>
        /// <returns>
        /// The method name to execute when the view is loaded.
        /// </returns>
        public static string GetMethodNameToExecute(DependencyObject view)
        {
            return (string)view.GetValue(ExecuteMethodOnLoadedBehaviour.MethodNameToExecuteProperty);
        }

        /// <summary>
        /// Sets the method name to execute when the view is loaded.
        /// </summary>
        /// <param name="view">The view.</param>
        /// <param name="value">The method name to execute when the view is loaded.</param>
        public static void SetMethodNameToExecute(DependencyObject view, string value)
        {
            view.SetValue(ExecuteMethodOnLoadedBehaviour.MethodNameToExecuteProperty, value);
        }

        /// <summary>
        /// Called when the 'MethodNameToExecute' property is changed.
        /// </summary>
        /// <param name="view">The view.</param>
        /// <param name="e">The <see cref="DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void MethodNameToExecuteChanged(
            DependencyObject view,
            DependencyPropertyChangedEventArgs e)
        {
            FrameworkElement viewControl = view as FrameworkElement;
            if (viewControl != null)
            {
                viewControl.Loaded += (sender, args) =>
                {
                    object viewModel = viewControl.DataContext;
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