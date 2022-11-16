using Pathfinding.App.WPF._2D.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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
