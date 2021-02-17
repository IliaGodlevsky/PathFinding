using System.Windows;

namespace WPFVersion.View.Windows
{
    /// <summary>
    /// Логика взаимодействия для GraphParametresWindow.xaml
    /// </summary>
    public partial class GraphCreatesWindow : Window
    {
        public GraphCreatesWindow()
        {
            InitializeComponent();
            obstacleSlider.Minimum = Constants.ObstaclesPercentValueRange.LowerValueOfRange;
            obstacleSlider.Maximum = Constants.ObstaclesPercentValueRange.UpperValueOfRange;
        }
    }
}
