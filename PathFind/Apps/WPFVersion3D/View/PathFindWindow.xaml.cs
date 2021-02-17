using System.Windows;

namespace WPFVersion3D.View
{
    /// <summary>
    /// Логика взаимодействия для PathFindWindow.xaml
    /// </summary>
    public partial class PathFindWindow : Window
    {
        public PathFindWindow()
        {
            InitializeComponent();
            delayTimeSlider.Minimum = Constants.AlgorithmDelayValueRange.LowerValueOfRange;
            delayTimeSlider.Maximum = Constants.AlgorithmDelayValueRange.UpperValueOfRange;
        }
    }
}
