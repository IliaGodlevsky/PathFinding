using Algorithm.Interfaces;
using GraphViewModel.Interfaces;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using WPFVersion.Enums;

namespace WPFVersion.ViewModel
{
    internal class AlgorithmViewModel : INotifyPropertyChanged, IModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string statistics;
        public string Statistics
        {
            get => statistics;
            set { statistics = value; OnPropertyChanged(); }
        }

        private AlgorithmStatus status;
        public AlgorithmStatus Status
        {
            get => status;
            set { status = value; OnPropertyChanged(); }
        }

        public AlgorithmViewModel(IAlgorithm algorithm)
        {
            this.algorithm = algorithm;
        }

        public void Interrupt()
        {
            algorithm.Interrupt();
            Status = AlgorithmStatus.Interrupted;
        }

        private readonly IAlgorithm algorithm;
    }
}
