using GraphViewModel.Interfaces;
using System.Windows;
using WPFVersion.Attributes;

namespace WPFVersion
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
