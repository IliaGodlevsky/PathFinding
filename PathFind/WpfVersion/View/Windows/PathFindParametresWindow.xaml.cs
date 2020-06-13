using GraphLibrary.Enums.AlgorithmEnum;
using System.Windows;
using WpfVersion.ViewModel;

namespace WpfVersion.View.Windows
{
    /// <summary>
    /// Логика взаимодействия для PathFindParametresWindow.xaml
    /// </summary>
    public partial class PathFindParametresWindow : Window
    {
        public PathFindParametresWindow()
        {
            InitializeComponent();
        }

        public void ChooseDijkstraAlgorithm(object sender, RoutedEventArgs e)
        {
            (DataContext as WpfVersionViewModel).Algorithm = Algorithms.DijkstraAlgorithm;
        }

        public void ChooseAStartALgorithm(object sender, RoutedEventArgs e)
        {
            (DataContext as WpfVersionViewModel).Algorithm = Algorithms.AStarAlgorithm;
        }

        public void ChooseWidePathFindAlgorithm(object sender, RoutedEventArgs e)
        {
            (DataContext as WpfVersionViewModel).Algorithm = Algorithms.WidePathFind;
        }

        public void ChooseDeepPathFindAlgorithm(object sender, RoutedEventArgs e)
        {
            (DataContext as WpfVersionViewModel).Algorithm = Algorithms.DeepPathFind;
        }
    }
}
