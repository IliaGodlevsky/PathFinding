using System.Windows;
using WPFVersion3D.Attributes;
using WPFVersion3D.ViewModel;

namespace WPFVersion3D.View
{
    /// <summary>
    /// Логика взаимодействия для PathFindWindow.xaml
    /// </summary>
    [AppWindow]
    public partial class PathFindWindow : Window
    {
        public PathFindWindow(PathFindingViewModel viewModel)
        {
            InitializeComponent();
            delayTimeSlider.Minimum = Constants.AlgorithmDelayValueRange.LowerValueOfRange;
            delayTimeSlider.Maximum = Constants.AlgorithmDelayValueRange.UpperValueOfRange;
            DataContext = viewModel;
            viewModel.WindowClosed += (sender, e) => Close();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }
    }
}
