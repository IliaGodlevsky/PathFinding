using WPFVersion.ViewModel;

namespace WPFVersion.View.Windows
{
    public partial class PathFindWindow : ViewModelWindow
    {
        public PathFindWindow(PathFindingViewModel viewModel) : base(viewModel)
        {
            InitializeComponent();
            delayTimeSlider.Minimum = Constants.AlgorithmDelayTimeValueRange.LowerValueOfRange.Milliseconds;
            delayTimeSlider.Maximum = Constants.AlgorithmDelayTimeValueRange.UpperValueOfRange.Milliseconds;
        }
    }
}
