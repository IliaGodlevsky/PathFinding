using WPFVersion3D.ViewModel;

namespace WPFVersion3D.View
{
    public partial class OpacityChangeWindow : ViewModelWindow
    {
        public OpacityChangeWindow(OpacityChangeViewModel viewModel) : base(viewModel)
        {
            InitializeComponent();           
        }
    }
}
