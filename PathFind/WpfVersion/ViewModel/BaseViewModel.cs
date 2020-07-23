using GraphLibrary.Constants;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace WpfVersion.ViewModel
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public virtual void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected virtual void AdjustSizeOfMainWindow(int graphWidth, int graphHeight)
        {
            Application.Current.MainWindow.Width = (graphWidth + 1) * Const.SIZE_BETWEEN_VERTICES + Const.SIZE_BETWEEN_VERTICES;
            Application.Current.MainWindow.Height = (1 + graphHeight) * Const.SIZE_BETWEEN_VERTICES +
                Application.Current.MainWindow.DesiredSize.Height;
        }
    }
}
