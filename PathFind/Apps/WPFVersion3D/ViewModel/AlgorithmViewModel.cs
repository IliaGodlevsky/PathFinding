using Algorithm.Base;
using Common.Extensions;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using WPFVersion3D.Enums;

namespace WPFVersion3D.ViewModel
{
    internal class AlgorithmViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly PathfindingAlgorithm algorithm;

        private string time;
        private int pathLength;
        private double pathCost;
        private int visitedVerticesCount;
        private AlgorithmStatuses status;

        public bool IsStarted => algorithm.IsInProcess;

        public string AlgorithmName { get; }
       
        public string Time
        {
            get => time;
            set { time = value; OnPropertyChanged(); }
        }

        public int PathLength
        {
            get => pathLength;
            set { pathLength = value; OnPropertyChanged(); }
        }

        public double PathCost
        {
            get => pathCost;
            set { pathCost = value; OnPropertyChanged(); }
        }

        public int VisitedVerticesCount
        {
            get => visitedVerticesCount;
            set { visitedVerticesCount = value; OnPropertyChanged(); }
        }

        public AlgorithmStatuses Status
        {
            get => status;
            set { status = value; OnPropertyChanged(); }
        }

        public AlgorithmViewModel(PathfindingAlgorithm algorithm)
        {
            this.algorithm = algorithm;
            AlgorithmName = algorithm.GetDescription();
        }

        public void Interrupt()
        {
            algorithm.Interrupt();
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}