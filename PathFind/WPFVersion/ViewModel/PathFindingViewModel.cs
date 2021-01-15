using Algorithm.AlgorithmCreating;
using Common.Interfaces;
using GraphLib.ViewModel;
using GraphViewModel.Interfaces;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using WPFVersion.Infrastructure;

namespace WPFVersion.ViewModel
{
    internal class PathFindingViewModel : PathFindingModel, IViewModel, INotifyPropertyChanged
    {
        public event EventHandler OnWindowClosed;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private ObservableCollection<string> algorithmKeys;
        public ObservableCollection<string> AlgorithmKeys 
        {
            get => algorithmKeys;
            set { algorithmKeys = value; OnPropertyChanged(); }
        }

        public ICommand ConfirmPathFindAlgorithmChoice { get; }
        public ICommand CancelPathFindAlgorithmChoice { get; }

        public PathFindingViewModel(IMainModel model) : base(model)
        {
            ConfirmPathFindAlgorithmChoice = new RelayCommand(
                ExecuteConfirmPathFindAlgorithmChoice,
                CanExecuteConfirmPathFindAlgorithmChoice);

            CancelPathFindAlgorithmChoice = new RelayCommand(obj => CloseWindow());

            AlgorithmKeys = new ObservableCollection<string>(AlgorithmFactory.AlgorithmsDescriptions);
            
            pauseProvider.PauseEvent += () => System.Windows.Forms.Application.DoEvents();
        }

        private void CloseWindow()
        {
            OnWindowClosed?.Invoke(this, new EventArgs());
        }

        private void ExecuteConfirmPathFindAlgorithmChoice(object param)
        {
            CloseWindow();
            base.FindPath();
        }

        private bool CanExecuteConfirmPathFindAlgorithmChoice(object param)
        {
            return AlgorithmKeys.Any(algo => algo == AlgorithmKey);
        }
    }
}
