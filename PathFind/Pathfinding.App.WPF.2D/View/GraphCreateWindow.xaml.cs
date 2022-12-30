using Pathfinding.App.WPF._2D.ViewModel;

namespace Pathfinding.App.WPF._2D.View
{
    /// <summary>
    /// Interaction logic for GraphCreateWindow.xaml
    /// </summary>
    public partial class GraphCreateWindow : ViewModelWindow
    {
        public GraphCreateWindow(GraphCreatingViewModel viewModel) : base(viewModel)
        {
            InitializeComponent();
        }
    }
}
