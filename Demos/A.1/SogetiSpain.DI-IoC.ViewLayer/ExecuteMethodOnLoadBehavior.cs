namespace SogetiSpain.DI_IoC.ViewLayer
{
    using System.Reflection;
    using System.Windows;

    public static class ExecuteMethodOnLoadBehavior
    {
        private static readonly DependencyProperty MethodNameToExecuteProperty =
            DependencyProperty.RegisterAttached(
                "MethodNameToExecute",
                typeof(string),
                typeof(ExecuteMethodOnLoadBehavior),
                new PropertyMetadata(null, ExecuteMethodOnLoadBehavior.OnMethodNameToExecuteChanged));

        public static string GetMethodNameToExecute(DependencyObject dependencyObject)
        {
            return (string)dependencyObject.GetValue(ExecuteMethodOnLoadBehavior.MethodNameToExecuteProperty);
        }

        public static void SetMethodNameToExecute(DependencyObject dependencyObject, string value)
        {
            dependencyObject.SetValue(ExecuteMethodOnLoadBehavior.MethodNameToExecuteProperty, value);
        }

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
    }
}
