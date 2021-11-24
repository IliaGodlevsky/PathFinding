using Common.Interface;
using System;
using System.Windows.Forms;
using WindowsFormsVersion.Attributes;

namespace WindowsFormsVersion.View
{
    [AppWindow]
    public abstract class ViewModelWindow : Form
    {
        public IViewModel DataContext { get; set; }

        protected ViewModelWindow(IViewModel viewModel)
        {
            DataContext = viewModel;
            viewModel.WindowClosed += Close;
            StartPosition = FormStartPosition.CenterScreen;
        }

        protected override void OnClosed(EventArgs e)
        {
            (DataContext as IDisposable)?.Dispose();
            base.OnClosed(e);
        }
    }
}
