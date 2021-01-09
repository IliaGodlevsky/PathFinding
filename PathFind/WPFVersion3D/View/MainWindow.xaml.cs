using System.Windows;
using WPFVersion3D.ViewModel;

namespace WPFVersion3D
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var model = new MainWindowViewModel();
            DataContext = model;

            distanceBetweenAtXAxisSlider.ValueChanged += model.XAxisSliderValueChanged;
            distanceBetweenAtYAxisSlider.ValueChanged += model.YAxisSliderValueChanged;
            distanceBetweenAtZAxisSlider.ValueChanged += model.ZAxisSliderValueChanged;
        }
    }
}
