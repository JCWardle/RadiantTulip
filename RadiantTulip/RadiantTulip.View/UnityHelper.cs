using Microsoft.Practices.Unity;
using System.Windows;

namespace RadiantTulip.View
{
    public class UnityHelper
    {
        public static IUnityContainer GetContainer(DependencyObject obj)
        {
            return (IUnityContainer)obj.GetValue(ContainerProperty);
        }

        public static void SetContainer(DependencyObject obj, IUnityContainer value)
        {
            obj.SetValue(ContainerProperty, value);
        }

        public static readonly DependencyProperty ContainerProperty = DependencyProperty.RegisterAttached("Container", typeof(IUnityContainer), typeof(UnityHelper), new FrameworkPropertyMetadata
        {
            Inherits = true,
            PropertyChangedCallback = (obj, e) =>
            {
                var container = e.NewValue as IUnityContainer;
                if (container != null)
                {
                    var element = obj as FrameworkElement;
                    container.BuildUp(obj.GetType(), obj, element == null ? null : element.Name);
                }
            }
        });
    }
}
