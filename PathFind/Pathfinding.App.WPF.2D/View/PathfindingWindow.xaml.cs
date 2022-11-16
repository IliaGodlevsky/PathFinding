using Pathfinding.App.WPF._2D.ViewModel;

namespace Pathfinding.App.WPF._2D.View
{
    /// <summary>
    /// Interaction logic for PathfindingWindow.xaml
    /// </summary>
    public partial class PathfindingWindow : ViewModelWindow
    {
        public PathfindingWindow(PathFindingViewModel viewModel) : base(viewModel)
        {
            InitializeComponent();
            delayTimeSlider.Minimum = Constants.AlgorithmDelayTimeValueRange.LowerValueOfRange.Milliseconds;
            delayTimeSlider.Maximum = Constants.AlgorithmDelayTimeValueRange.UpperValueOfRange.Milliseconds;
        }
    }
}
