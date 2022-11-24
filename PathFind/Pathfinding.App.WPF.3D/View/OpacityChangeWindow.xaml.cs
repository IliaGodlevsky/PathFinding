using Pathfinding.App.WPF._3D.Interface;
using Pathfinding.App.WPF._3D.ViewModel;
using Shared.Extensions;
using System.Linq;
using System.Windows;

using static System.Reflection.BindingFlags;

namespace Pathfinding.App.WPF._3D.View
{
    /// <summary>
    /// Interaction logic for OpacityChangeWindow.xaml
    /// </summary>
    public partial class OpacityChangeWindow : ViewModelWindow
    {
        public OpacityChangeWindow(OpacityChangeViewModel viewModel) 
            : base(viewModel)
        {
            InitializeComponent();
            viewModel.OpacityChangers = GetType()
                .GetFields(NonPublic | Instance)
                .Where(field => field.FieldType == typeof(VertexOpacityUserControl))
                .Select(field => (FrameworkElement)field.GetValue(this))
                .Select(control => (IChangeColorOpacity)control.DataContext)
                .ToReadOnly();
        }
    }
}
