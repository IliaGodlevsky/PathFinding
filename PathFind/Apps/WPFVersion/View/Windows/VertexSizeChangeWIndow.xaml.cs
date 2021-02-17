using System.Windows;

namespace WPFVersion.View.Windows
{
    /// <summary>
    /// Логика взаимодействия для VertexSizeChangeWIndow.xaml
    /// </summary>
    public partial class VertexSizeChangeWindow : Window
    {
        public VertexSizeChangeWindow()
        {
            InitializeComponent();
            sizeSlider.Minimum = Constants.VertexSizeRange.LowerValueOfRange;
            sizeSlider.Maximum = Constants.VertexSizeRange.UpperValueOfRange;
        }
    }
}
