using System.Windows;

namespace WPFVersion
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            xSlider.Minimum = Constants.OffsetValueRange.LowerValueOfRange;
            xSlider.Maximum = Constants.OffsetValueRange.UpperValueOfRange;
            ySlider.Minimum = Constants.OffsetValueRange.LowerValueOfRange;
            ySlider.Maximum = Constants.OffsetValueRange.UpperValueOfRange;
        }
    }
}
