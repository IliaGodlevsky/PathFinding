using WPFVersion3D.ViewModel;

namespace WPFVersion3D.View
{
    public partial class PathFindWindow : ViewModelWindow
    {
        public PathFindWindow(PathFindingViewModel viewModel) : base(viewModel)
        {
            InitializeComponent();
            delayTimeSlider.Minimum = Constants.AlgorithmDelayValueRange.LowerValueOfRange.TotalMilliseconds;
            delayTimeSlider.Maximum = Constants.AlgorithmDelayValueRange.UpperValueOfRange.TotalMilliseconds;
        }
    }
}
