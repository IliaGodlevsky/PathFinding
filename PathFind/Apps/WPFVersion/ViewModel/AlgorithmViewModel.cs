using Algorithm.Interfaces;
using GalaSoft.MvvmLight.Messaging;
using GraphViewModel.Interfaces;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using WPFVersion.Enums;
using WPFVersion.Messages;

namespace WPFVersion.ViewModel
{
    internal class AlgorithmViewModel : INotifyPropertyChanged, IModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public int Index { get; }
        public string AlgorithmName { get; }

        private string time;
        public string Time
        {
            get => time;
            set { time = value; OnPropertyChanged(); }
        }

        private int delayTime;
        public int DelayTime
        {
            get => delayTime;
            set
            {
                delayTime = (int)Constants.AlgorithmDelayTimeValueRange.ReturnInRange(value);
                OnPropertyChanged();
                var message = new DelayTimeChangedMessage(delayTime, Index);
                Messenger.Default.Send(message, MessageTokens.PathfindingModel);
            }
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

        public AlgorithmViewModel(IAlgorithm algorithm, string algorithmName, int delayTime, int index)
        {
            this.algorithm = algorithm;
            AlgorithmName = algorithmName;
            Status = AlgorithmStatus.Started;
            DelayTime = delayTime;
            Index = index;
        }

        public AlgorithmViewModel(AlgorithmStartedMessage message, int index)
            : this(message.Algorithm, message.AlgorithmName, message.DelayTime, index)
        {

        }

        public void Interrupt()
        {
            algorithm.Interrupt();
            Status = AlgorithmStatus.Interrupted;
        }

        private readonly IAlgorithm algorithm;
    }
}
