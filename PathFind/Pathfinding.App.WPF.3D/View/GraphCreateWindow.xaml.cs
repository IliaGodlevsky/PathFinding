using Pathfinding.App.WPF._3D.ViewModel;

namespace Pathfinding.App.WPF._3D.View
{
    /// <summary>
    /// Interaction logic for GraphCreateWindow.xaml
    /// </summary>
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
