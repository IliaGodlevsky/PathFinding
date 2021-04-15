using GraphLib.Interfaces;
using System.Windows;
using System.Windows.Media.Media3D;

namespace WPFVersion3D.Model
{
    internal sealed class GraphField3DContainer : ModelVisual3D
    {
        public static readonly DependencyProperty GraphFieldProperty;

        static GraphField3DContainer()
        {
            GraphFieldProperty = DependencyProperty.Register(
                nameof(GraphField),
                typeof(IGraphField),
                typeof(GraphField3DContainer),
                new PropertyMetadata(OnGraphFieldChanged));
        }

        public IGraphField GraphField
        {
            get => (GraphField3D)GetValue(GraphFieldProperty);
            set => SetValue(GraphFieldProperty, value);
        }

        private static void OnGraphFieldChanged(DependencyObject depObj,
            DependencyPropertyChangedEventArgs args)
        {
            if (args.NewValue is GraphField3D child && depObj is GraphField3DContainer field)
            {
                field.Children.Clear();
                field.Children.Add(child);
            }
        }
    }
}
