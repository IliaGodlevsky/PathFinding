using Algorithm.Common;
using Algorithm.Common.Exceptions;
using Algorithm.Extensions;
using Algorithm.Infrastructure.EventArguments;
using Algorithm.Interfaces;
using Common;
using Common.Extensions;
using Common.Interface;
using Common.Logging;
using GraphLib.Base;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using GraphViewModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using static Algorithm.Realizations.AlgorithmsFactory;
using static GraphViewModel.Resources.ViewModelResources;

namespace GraphViewModel
{
    public abstract class PathFindingModel : IModel
    {
        public event Action<string> OnEventHappened;

        public BaseEndPoints EndPoints { get; set; }

        public int DelayTime { get; set; }

        public string AlgorithmKey { get; set; }

        public virtual IList<string> AlgorithmKeys { get; set; }

        protected PathFindingModel(IMainModel mainViewModel)
        {
            this.mainViewModel = mainViewModel;
            DelayTime = 4;
            timer = new Stopwatch();
            path = new NullGraphPath();
            algorithm = new DefaultAlgorithm();
        }

        public virtual void FindPath()
        {
            try
            {
                algorithm = GetAlgorithm(AlgorithmKey);
                algorithm.Graph = mainViewModel.Graph;
                interrupter = new Interrupter();
                SubscribeOnAlgorithmEvents();
                path = algorithm.FindPath(EndPoints);
                Summarize();
            }
            catch (AlgorithmInterruptedException ex)
            {
                RaiseOnEventHappened(ex.Message);
            }
            catch (Exception ex)
            {
                RaiseOnEventHappened(ex.Message);
                Logger.Instance.Error(ex);
            }
            finally
            {
                algorithm.Reset();
                EndPoints.Reset();
            }
        }

        protected void RaiseOnEventHappened(string message)
        {
            OnEventHappened?.Invoke(message);
        }

        protected abstract void ColorizeProcessedVertices();

        protected virtual void OnVertexVisited(object sender, EventArgs e)
        {
            if (e is AlgorithmEventArgs args)
            {
                if (!args.IsEndPoint && args.Vertex is IMarkable vertex)
                {
                    vertex.MarkAsVisited();
                }

                visitedVerticesCount = args.VisitedVertices;
                string statistics = GetStatistics(timer);
                mainViewModel.PathFindingStatistics = statistics;
            }

            ColorizeProcessedVertices();

            interrupter.Suspend(DelayTime);
        }

        protected virtual void OnVertexEnqueued(object sender, EventArgs e)
        {
            if (e is AlgorithmEventArgs args)
            {
                if (!args.IsEndPoint && args.Vertex is IMarkable vertex)
                {
                    vertex.MarkAsEnqueued();
                }
            }
        }

        protected virtual void OnAlgorithmInterrupted(object sender, EventArgs e)
        {
            timer.Stop();
            string message = $"Algorithm {AlgorithmKey} was interrupted";
            Logger.Instance.Info(message);
            throw new AlgorithmInterruptedException(AlgorithmInterruptedMsg);
        }

        protected virtual void OnAlgorithmFinished(object sender, EventArgs e)
        {
            if (e is AlgorithmEventArgs args)
            {
                visitedVerticesCount = args.VisitedVertices;
                string message = $"Algorithm { AlgorithmKey } was finished";
                Logger.Instance.Info(message);
            }

            timer.Stop();
        }

        protected virtual void Summarize()
        {
            if (path.IsExtracted())
            {
                path.Highlight(EndPoints);
                string statistics = GetStatistics(timer, path);
                mainViewModel.PathFindingStatistics = statistics;
            }
            else
            {
                OnEventHappened?.Invoke(PathWasNotFoundMsg);
                Logger.Instance.Info(PathWasNotFoundMsg);
            }
        }

        protected virtual void OnAlgorithmStarted(object sender, EventArgs e)
        {
            string message = $"Algorithm {AlgorithmKey} was started. ";
            message += $"Start vertex: {EndPoints.Start.GetInforamtion()};";
            message += $"End vertex: {EndPoints.End.GetInforamtion()}";
            Logger.Instance.Info(message);
            timer.Reset();
            timer.Start();
        }

        protected ISuspendable interrupter;
        protected IMainModel mainViewModel;
        protected IAlgorithm algorithm;
        protected IGraphPath path;

        private string GetStatistics(Stopwatch timer, IGraphPath path = null)
        {
            int pathLength = path?.GetPathLength() ?? 0;
            int pathCost = path?.GetPathCost() ?? 0;
            string graphInfo = string.Format(StatisticsFormat,
                pathLength, pathCost, visitedVerticesCount);
            string timerInfo = timer.GetTimeInformation(TimerInfoFormat);
            return string.Join(Separator, AlgorithmKey, timerInfo, graphInfo);
        }

        private void SubscribeOnAlgorithmEvents()
        {
            algorithm.OnVertexEnqueued += OnVertexEnqueued;
            algorithm.OnVertexVisited += OnVertexVisited;
            algorithm.OnFinished += OnAlgorithmFinished;
            algorithm.OnStarted += OnAlgorithmStarted;
            algorithm.OnInterrupted += OnAlgorithmInterrupted;
        }

        private int visitedVerticesCount;
        private const string Separator = "   ";
        private readonly Stopwatch timer;

    }
}
