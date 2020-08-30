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
            obstacleSlider.Minimum = GraphParametresRange.LowerObstacleValue;
            obstacleSlider.Maximum = GraphParametresRange.UpperObstacleValue;
        }
    }
}
