using System.Linq;
using System.Windows.Controls;
using WPFVersion3D.Interface;
using WPFVersion3D.ViewModel;

using static System.Reflection.BindingFlags;

namespace WPFVersion3D.View
{
    public partial class OpacityChangeWindow : ViewModelWindow
    {
        public OpacityChangeWindow(OpacityChangeViewModel viewModel) : base(viewModel)
        {
            InitializeComponent();
            viewModel.OpacityChangers =
                GetType()
                .GetFields(NonPublic | Instance)
                .Where(field => field.FieldType == typeof(VertexOpacityUserControl))
                .Select(field => (UserControl)field.GetValue(this))
                .Select(control => (IChangeColorOpacity)control.DataContext)
                .ToArray();
        }
    }
}
