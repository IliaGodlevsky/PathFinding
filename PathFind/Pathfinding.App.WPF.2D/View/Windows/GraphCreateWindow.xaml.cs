using Pathfinding.App.WPF._2D.ViewModel;

namespace WPFVersion.View.Windows
{
    public partial class GraphCreatesWindow : ViewModelWindow
    {
        public GraphCreatesWindow(GraphCreatingViewModel viewModel) : base(viewModel)
        {
            InitializeComponent();
        }
    }
}
