using WPFVersion3D.Attributes;
using WPFVersion3D.ViewModel;

namespace WPFVersion3D.View
{
    public partial class GraphCreateWindow : ViewModelWindow
    {
        public GraphCreateWindow(GraphCreatingViewModel viewModel) : base(viewModel)
        {
            InitializeComponent();
            obstacleSlider.Minimum = Constants.ObstaclePercentValueRange.LowerValueOfRange;
            obstacleSlider.Maximum = Constants.ObstaclePercentValueRange.UpperValueOfRange;            
        }
    }
}
