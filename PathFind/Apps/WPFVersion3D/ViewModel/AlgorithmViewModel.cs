using Algorithm.Interfaces;
using GraphViewModel.Interfaces;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using WPFVersion3D.Enums;

namespace WPFVersion3D.ViewModel
{
    internal class AlgorithmViewModel : INotifyPropertyChanged, IModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string algorithmName;
        public string AlgorithmName
        {
            get => algorithmName;
            private set { algorithmName = value; OnPropertyChanged(); }
        }

        private string time;
        public string Time
        {
            get => time;
            set { time = value; OnPropertyChanged(); }
        }

        private int pathLength;
        public int PathLength
        {
            get => pathLength;
            set { pathLength = value; OnPropertyChanged(); }
        }

        private double pathCost;
        public double PathCost
        {
            get => pathCost;
            set { pathCost = value; OnPropertyChanged(); }
        }

        private int visitedVerticesCount;
        public int VisitedVerticesCount
        {
            get => visitedVerticesCount;
            set { visitedVerticesCount = value; OnPropertyChanged(); }
        }

        private AlgorithmStatus status;
        public AlgorithmStatus Status
        {
            get => status;
            set { status = value; OnPropertyChanged(); }
        }

        public AlgorithmViewModel(IAlgorithm algorithm, string algorithmName)
        {
            this.algorithm = algorithm;
            this.algorithmName = algorithmName;
        }

        public void Interrupt()
        {
            algorithm.Interrupt();
            Status = AlgorithmStatus.Interrupted;
        }

        private readonly IAlgorithm algorithm;
    }
}
