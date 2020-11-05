using System.Windows;
using Wpf3dVersion.ViewModel;

namespace Wpf3dVersion
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainWindowViewModel model = new MainWindowViewModel();
            DataContext = model;
            distanceBetweenAtXAxisSlider.ValueChanged += model.XAxisSliderValueChanged;
            distanceBetweenAtYAxisSlider.ValueChanged += model.YAxisSliderValueChanged;
            distanceBetweenAtZAxisSlider.ValueChanged += model.ZAxisSliderValueChanged;
        }       
    }
}
