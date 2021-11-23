using GraphViewModel.Interfaces;
using System.Windows;
using WPFVersion3D.Attributes;

namespace WPFVersion3D
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    [AppWindow]
    public partial class MainWindow : Window
    {
        public MainWindow(IMainModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
