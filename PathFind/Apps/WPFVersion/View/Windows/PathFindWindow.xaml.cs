using System.Windows;
using WPFVersion.Attributes;
using WPFVersion.ViewModel;

namespace WPFVersion.View.Windows
{
    /// <summary>
    /// Логика взаимодействия для PathFindParametresWindow.xaml
    /// </summary>
    [AppWindow]
    public partial class PathFindWindow : Window
    {

        public PathFindWindow(PathFindingViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
            viewModel.WindowClosed += (sender, args) => Close();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            delayTimeSlider.Minimum = Constants.AlgorithmDelayTimeValueRange.LowerValueOfRange;
            delayTimeSlider.Maximum = Constants.AlgorithmDelayTimeValueRange.UpperValueOfRange;
        }
    }
}
