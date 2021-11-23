using System.Windows;
using WPFVersion3D.Attributes;
using WPFVersion3D.ViewModel;

namespace WPFVersion3D.View
{
    /// <summary>
    /// Логика взаимодействия для OpacityChangeWindow.xaml
    /// </summary>
    [AppWindow]
    public partial class OpacityChangeWindow : Window
    {
        public OpacityChangeWindow(OpacityChangeViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
            viewModel.WindowClosed += (sender, e) => Close();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }
    }
}
