using Algorithm.Base;
using Algorithm.Interfaces;
using Autofac;
using Common.Extensions;
using GalaSoft.MvvmLight.Messaging;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using ValueRange.Enums;
using ValueRange.Extensions;
using WPFVersion.DependencyInjection;
using WPFVersion.Enums;
using WPFVersion.Extensions;
using WPFVersion.Messages;

namespace WPFVersion.ViewModel
{
    internal class AlgorithmViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public bool IsStarted => algorithm.IsInProcess;
        public bool IsPaused => algorithm.IsPaused;
        public IAlgorithm Algorithm => algorithm;
        public int Index { get; }
        public string AlgorithmName { get; }

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
                delayTime = (int)Constants.AlgorithmDelayTimeValueRange.ReturnInRange(value, ReturnOptions.Cycle);
                OnPropertyChanged();
                var message = new DelayTimeChangedMessage(delayTime, Index);
                messenger.ForwardParallel(message, MessageTokens.PathfindingModel);
            }
        }

        public AlgorithmViewModel(PathfindingAlgorithm algorithm, int delayTime, int index)
        {
            this.algorithm = algorithm;
            this.delayTime = delayTime;
            AlgorithmName = algorithm.GetDescription();
            Status = AlgorithmStatus.Started;
            Index = index;
            messenger = DI.Container.Resolve<IMessenger>();
        }

        public AlgorithmViewModel(AlgorithmStartedMessage message, int index)
            : this(message.Algorithm, message.DelayTime, index)
        {

        }

        public void Interrupt()
        {
            algorithm.Interrupt();
        }

        public void Pause()
        {
            algorithm.Pause();
        }

        public void Unpause()
        {
            algorithm.Resume();
        }

        private readonly PathfindingAlgorithm algorithm;
        private readonly IMessenger messenger;
    }
}