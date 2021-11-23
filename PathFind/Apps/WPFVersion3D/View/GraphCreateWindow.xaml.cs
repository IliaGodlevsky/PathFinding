using System.Windows;
using WPFVersion3D.Attributes;
using WPFVersion3D.ViewModel;

namespace WPFVersion3D.View
{
    /// <summary>
    /// Логика взаимодействия для GraphCreateWindow.xaml
    /// </summary>
    [AppWindow]
    public partial class GraphCreateWindow : Window
    {
        public GraphCreateWindow(GraphCreatingViewModel viewModel)
        {
            InitializeComponent();
            obstacleSlider.Minimum = Constants.ObstaclePercentValueRange.LowerValueOfRange;
            obstacleSlider.Maximum = Constants.ObstaclePercentValueRange.UpperValueOfRange;
            DataContext = viewModel;
            viewModel.WindowClosed += (sender, e) => Close();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }
    }
}
