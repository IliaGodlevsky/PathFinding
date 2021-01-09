using Common.ValueRanges;
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
            sizeSlider.Minimum = Range.VertexSizeRange.LowerRange;
            sizeSlider.Maximum = Range.VertexSizeRange.UpperRange;
        }
    }
}
