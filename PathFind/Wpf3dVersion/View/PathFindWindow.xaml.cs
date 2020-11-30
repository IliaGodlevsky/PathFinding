using Common.ValueRanges;
using System.Windows;

namespace Wpf3dVersion.View
{
    /// <summary>
    /// Логика взаимодействия для PathFindWindow.xaml
    /// </summary>
    public partial class PathFindWindow : Window
    {
        public PathFindWindow()
        {
            InitializeComponent();
            delayTimeSlider.Minimum = Range.DelayValueRange.LowerRange;
            delayTimeSlider.Maximum = Range.DelayValueRange.UpperRange;
        }
    }
}
