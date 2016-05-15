// ----------------------------------------------------------------------------
// <copyright file="ExampleBlendBehavior.cs" company="SOGETI Spain">
//     Copyright © 2016 SOGETI Spain. All rights reserved.
//     MVVM Course by Óscar Fernández González a.k.a. Osc@rNET.
// </copyright>
// ----------------------------------------------------------------------------
namespace SogetiSpain.MvvmCourse.UI
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Interactivity;

    /// <summary>
    /// Represents the Blend behavior for support a watermarks in a text box control.
    /// </summary>
    public class ExampleBlendBehavior : Behavior<TextBox>
    {
        #region Fields

        /// <summary>
        /// Defines the 'IsWatermarkEnabled' property.
        /// </summary>
        private static readonly DependencyProperty IsWatermarkEnabledProperty =
            DependencyProperty.RegisterAttached(
                "IsWatermarkEnabled",
                typeof(bool),
                typeof(ExampleBlendBehavior),
                new UIPropertyMetadata(false));

        /// <summary>
        /// Defines the 'WatermarkText' property.
        /// </summary>
        private static readonly DependencyProperty WatermarkTextProperty =
            DependencyProperty.RegisterAttached(
                "WatermarkText",
                typeof(string),
                typeof(ExampleBlendBehavior),
                new UIPropertyMetadata(string.Empty));

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether the watermark is enabled.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the watermark is enabled; otherwise, <c>false</c>.
        /// </value>
        public bool IsWatermarkEnabled
        {
            get
            {
                return (bool)this.GetValue(ExampleBlendBehavior.IsWatermarkEnabledProperty);
            }

            set
            {
                this.SetValue(ExampleBlendBehavior.IsWatermarkEnabledProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the watermark text.
        /// </summary>
        /// <value>
        /// The watermark text.
        /// </value>
        public string WatermarkText
        {
            get
            {
                return (string)this.GetValue(ExampleBlendBehavior.WatermarkTextProperty);
            }

            set
            {
                this.SetValue(ExampleBlendBehavior.WatermarkTextProperty, value);
            }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Called after the behavior is attached to an AssociatedObject.
        /// </summary>
        /// <remarks>
        /// Override this to hook up functionality to the AssociatedObject.
        /// </remarks>
        protected override void OnAttached()
        {
            this.AssociatedObject.GotFocus += this.OnTextBoxGotFocus;
            this.AssociatedObject.LostFocus += this.OnTextBoxLostFocus;

            if (this.IsWatermarkEnabled &&
                string.IsNullOrEmpty(this.AssociatedObject.Text))
            {
                this.AssociatedObject.Text = this.WatermarkText;
            }
        }

        /// <summary>
        /// Called when the behavior is being detached from its AssociatedObject, but before it has actually occurred.
        /// </summary>
        /// <remarks>
        /// Override this to unhook functionality from the AssociatedObject.
        /// </remarks>
        protected override void OnDetaching()
        {
            this.AssociatedObject.GotFocus -= this.OnTextBoxGotFocus;
            this.AssociatedObject.LostFocus -= this.OnTextBoxLostFocus;
        }

        /// <summary>
        /// Called when the text box got focus.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void OnTextBoxGotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = e.OriginalSource as TextBox;

            if (!this.IsWatermarkEnabled || (textBox == null))
            {
                return;
            }

            if (string.Equals(textBox.Text, this.WatermarkText))
            {
                textBox.Text = string.Empty;
            }
        }

        /// <summary>
        /// Called when the text box lost focus.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void OnTextBoxLostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = e.OriginalSource as TextBox;

            if (!this.IsWatermarkEnabled || (textBox == null))
            {
                return;
            }

            if (string.IsNullOrEmpty(textBox.Text))
            {
                textBox.Text = this.WatermarkText;
            }
        }

        #endregion Methods
    }
}