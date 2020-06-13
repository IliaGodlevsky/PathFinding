using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using WpfVersion.Infrastructure;
using WpfVersion.Model.Graph;
using WpfVersion.View.Windows;

namespace WpfVersion.ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private string statistics;

        public string Statistics 
        { 
            get
            {
                return statistics;
            }
            set
            {
                statistics = value; OnPropertyChanged();
            }
        }

        public Window Window { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private Canvas graphField;
        public Canvas GraphField { get { return graphField; } set { graphField = value; OnPropertyChanged(); } }
        public WpfGraph Graph { get; set; }

        public RelayCommand StartPathFindCommand { get; }
        public RelayCommand CreateNewGraphCommand { get; }
        public RelayCommand ClearGraphCommand { get; }
        public MainWindowViewModel()
        {
            GraphField = new Canvas();

            StartPathFindCommand = new RelayCommand(ExecuteStartPathFindCommand,
                obj => Graph?.End != null && Graph?.Start != null);

            CreateNewGraphCommand = new RelayCommand(ExecuteCreateNewGraphCommand, obj => true);

            ClearGraphCommand = new RelayCommand(ExecuteClearGraphCommand, 
                obj=> Graph != null);

        }

        private void ExecuteClearGraphCommand(object param)
        {
            Graph.Refresh();
            Statistics = string.Empty;
        }

        private void ExecuteStartPathFindCommand(object param)
        {
            PathFindViewModel model = new PathFindViewModel(this);
            Window = new PathFindWindow();
            Window.DataContext = model;
            Window.Show();
        }

        private void ExecuteCreateNewGraphCommand(object param)
        {
            Window = new GraphCreatesWindow();
            GraphParametresViewModel model = new GraphParametresViewModel(this);
            Window.DataContext = model;
            Window.Show();
        }

        public virtual void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Dispose()
        {
            OnDispose();
        }

        protected virtual void OnDispose()
        {
            return;
        }
    }
}
