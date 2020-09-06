using GraphLibrary.Common.Constants;
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
            obstacleSlider.Minimum = new ObstaclePercentRange().LowerRange;
            obstacleSlider.Maximum = new ObstaclePercentRange().UpperRange;
            var obstaclePercentRange = new ObstaclePercentRange();
        }
    }
}
