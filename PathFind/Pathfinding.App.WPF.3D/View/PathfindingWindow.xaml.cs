using Pathfinding.App.WPF._3D.ViewModel;

namespace Pathfinding.App.WPF._3D.View
{
    /// <summary>
    /// Interaction logic for PathfindingWindow.xaml
    /// </summary>
    public partial class PathfindingWindow : ViewModelWindow
    {
        public PathfindingWindow(PathFindingViewModel viewModel) : base(viewModel)
        {
            InitializeComponent();
            delayTimeSlider.Minimum = Constants.AlgorithmDelayValueRange.LowerValueOfRange.TotalMilliseconds;
            delayTimeSlider.Maximum = Constants.AlgorithmDelayValueRange.UpperValueOfRange.TotalMilliseconds;
        }
    }
}
