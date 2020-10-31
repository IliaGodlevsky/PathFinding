using Common.ValueRanges;
using System.Windows;

namespace WpfVersion.View.Windows
{
    /// <summary>
    /// Логика взаимодействия для GraphParametresWindow.xaml
    /// </summary>
    public partial class GraphCreatesWindow : Window
    {
        public GraphCreatesWindow()
        {
            InitializeComponent();
            obstacleSlider.Minimum = Range.ObstaclePercentValueRange.LowerRange;
            obstacleSlider.Maximum = Range.ObstaclePercentValueRange.UpperRange;
        }
    }
}
