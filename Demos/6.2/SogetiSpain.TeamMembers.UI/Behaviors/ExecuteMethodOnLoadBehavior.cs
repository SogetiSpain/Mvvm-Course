// ----------------------------------------------------------------------------
// <copyright file="ExecuteMethodOnLoadBehavior.cs" company="SOGETI Spain">
//     Copyright © 2016 SOGETI Spain. All rights reserved.
//     MVVM Course by Óscar Fernández González a.k.a. Osc@rNET.
// </copyright>
// ----------------------------------------------------------------------------
namespace SogetiSpain.TeamMembers.UI.Behaviors
{
    using System.Reflection;
    using System.Windows;

    /// <summary>
    /// Represents the execute method on load behavior.
    /// </summary>
    public static class ExecuteMethodOnLoadBehavior
    {
        #region Fields

        /// <summary>
        /// Defines the method name to execute property.
        /// </summary>
        private static readonly DependencyProperty MethodNameToExecuteProperty =
            DependencyProperty.RegisterAttached(
                "MethodNameToExecute",
                typeof(string),
                typeof(ExecuteMethodOnLoadBehavior),
                new PropertyMetadata(null, ExecuteMethodOnLoadBehavior.OnMethodNameToExecuteChanged));

        #endregion Fields

        #region Methods

        /// <summary>
        /// Gets the method name to execute.
        /// </summary>
        /// <param name="dependencyObject">The dependency object.</param>
        /// <returns>
        /// The method name to execute.
        /// </returns>
        public static string GetMethodNameToExecute(DependencyObject dependencyObject)
        {
            return (string)dependencyObject.GetValue(ExecuteMethodOnLoadBehavior.MethodNameToExecuteProperty);
        }

        /// <summary>
        /// Sets the method name to execute.
        /// </summary>
        /// <param name="dependencyObject">The dependency object.</param>
        /// <param name="value">The value.</param>
        public static void SetMethodNameToExecute(DependencyObject dependencyObject, string value)
        {
            dependencyObject.SetValue(ExecuteMethodOnLoadBehavior.MethodNameToExecuteProperty, value);
        }

        /// <summary>
        /// Called when the method name to execute property is changed.
        /// </summary>
        /// <param name="dependencyObject">The dependency object.</param>
        /// <param name="e">The <see cref="DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
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