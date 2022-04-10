using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Media3D;

namespace WPFVersion3D.View
{
    public partial class GraphFieldUserControl : UserControl
    {
        public static readonly DependencyProperty XAngleRotationProperty;
        public static readonly DependencyProperty YAngleRotationProperty;
        public static readonly DependencyProperty ZAngleRotationProperty;

        public AxisAngleRotation3D XAngleRotation
        {
            get => (AxisAngleRotation3D)GetValue(XAngleRotationProperty);
            set => SetValue(XAngleRotationProperty, value);
        }

        public AxisAngleRotation3D YAngleRotation
        {
            get => (AxisAngleRotation3D)GetValue(YAngleRotationProperty);
            set => SetValue(YAngleRotationProperty, value);
        }

        public AxisAngleRotation3D ZAngleRotation
        {
            get => (AxisAngleRotation3D)GetValue(ZAngleRotationProperty);
            set => SetValue(ZAngleRotationProperty, value);
        }

        public GraphFieldUserControl()
        {
            InitializeComponent();
        }

        static GraphFieldUserControl()
        {
            XAngleRotationProperty = RegisterProperty(nameof(XAngleRotation), XAngleRotationPropertyChanged);
            YAngleRotationProperty = RegisterProperty(nameof(YAngleRotation), YAngleRotationPropertyChanged);
            ZAngleRotationProperty = RegisterProperty(nameof(ZAngleRotation), ZAngleRotationPropertyChanged);
        }

        private static DependencyProperty RegisterProperty(string propertyName, PropertyChangedCallback changedCallback)
        {
            return DependencyProperty.Register(propertyName, typeof(AxisAngleRotation3D), typeof(GraphFieldUserControl), new PropertyMetadata(changedCallback));
        }

        protected static void XAngleRotationPropertyChanged(DependencyObject depObj, DependencyPropertyChangedEventArgs prop)
        {
            var control = (GraphFieldUserControl)depObj;
            control.XAngleRotation = (AxisAngleRotation3D)prop.NewValue;
        }

        protected static void YAngleRotationPropertyChanged(DependencyObject depObj, DependencyPropertyChangedEventArgs prop)
        {
            var control = (GraphFieldUserControl)depObj;
            control.YAngleRotation = (AxisAngleRotation3D)prop.NewValue;
        }

        protected static void ZAngleRotationPropertyChanged(DependencyObject depObj, DependencyPropertyChangedEventArgs prop)
        {
            var control = (GraphFieldUserControl)depObj;
            control.ZAngleRotation = (AxisAngleRotation3D)prop.NewValue;
        }
    }
}
