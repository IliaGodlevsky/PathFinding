using Common.ValueRanges;
using System.Windows;

namespace WPFVersion3D.View
{
    /// <summary>
    /// Логика взаимодействия для PathFindWindow.xaml
    /// </summary>
    public partial class PathFindWindow : Window
    {
        public PathFindWindow()
        {
            InitializeComponent();
            delayTimeSlider.Minimum = Range.DelayValueRange.LowerValueOfRange;
            delayTimeSlider.Maximum = Range.DelayValueRange.UpperValueOfRange;
        }
    }
}
