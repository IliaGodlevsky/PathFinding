using System.Windows;
using WPFVersion.Attributes;
using WPFVersion.ViewModel;

namespace WPFVersion.View.Windows
{
    /// <summary>
    /// Логика взаимодействия для GraphParametresWindow.xaml
    /// </summary>
    [AppWindow]
    public partial class GraphCreatesWindow : Window
    {
        public GraphCreatesWindow(GraphCreatingViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
            viewModel.WindowClosed += (sender, args) => Close();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }
    }
}
