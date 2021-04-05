using System;
using System.Collections.Generic;
using System.Diagnostics;
using Algorithm.Extensions;
using Algorithm.Infrastructure.EventArguments;
using Algorithm.Interfaces;
using Algorithm.Realizations;
using Common.Extensions;
using Common.Interface;
using Common.Logging;
using GraphLib.Base;
using GraphLib.Interface;
using GraphViewModel.Interfaces;
using GraphViewModel.Resources;
using static Algorithm.Realizations.AlgorithmsFactory;

namespace GraphViewModel
{
    public abstract class PathFindingModel : IModel
    {
        public event Action<string> OnPathNotFound;

        public BaseEndPoints EndPoints { get; set; }

        public int DelayTime { get; set; }

        public string AlgorithmKey { get; set; }

        public virtual IList<string> AlgorithmKeys { get; set; }

        public PathFindingModel(IMainModel mainViewModel)
        {
            this.mainViewModel = mainViewModel;
            DelayTime = 4;
            timer = new Stopwatch();
        }

        public virtual void FindPath()
        {
            try
            {
                var algorithm = GetAlgorithm(AlgorithmKey);
                algorithm.Graph = mainViewModel.Graph;

                intermitter = new Pause();
                intermitter.OnInterrupted += OnAlgorithmIntermitted;

                algorithm.OnVertexEnqueued += OnVertexEnqueued;
                algorithm.OnVertexVisited += OnVertexVisited;
                algorithm.OnFinished += OnAlgorithmFinished;
                algorithm.OnStarted += OnAlgorithmStarted;

                var path = algorithm.FindPath(EndPoints);

                var statistics = GetStatistics(timer, visitedVerticesCount, path);
                mainViewModel.PathFindingStatistics = statistics;
                TryHighlightPath(path);

                algorithm.Reset();
                EndPoints.Reset();
            }
            catch (Exception ex)
            {
                Logger.Instance.Error(ex);
            }
        }

        protected virtual bool TryHighlightPath(IGraphPath path)
        {
            if (path.IsExtracted())
            {
                path.HighlightPath(EndPoints);
                return true;
            }
            else
            {
                OnPathNotFound?.Invoke("Couldn't find path");
                return false;
            }
        }

        protected abstract void OnAlgorithmIntermitted();

        protected virtual void OnVertexVisited(object sender, EventArgs e)
        {
            if (e is AlgorithmEventArgs args)
            {
                if (!args.IsEndPoint && args.Vertex is IMarkable vertex)
                {
                    vertex.MarkAsVisited();
                }

                var statistics = GetStatistics(timer, args.VisitedVertices);
                mainViewModel.PathFindingStatistics = statistics;
            }

            intermitter.Suspend(DelayTime);
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

        protected virtual void OnAlgorithmFinished(object sender, EventArgs e)
        {
            if (e is AlgorithmEventArgs args)
            {
                visitedVerticesCount = args.VisitedVertices;
            }
            timer.Stop();
        }

        protected virtual void OnAlgorithmStarted(object sender, EventArgs e)
        {
            timer.Reset();
            timer.Start();
        }

        protected ISuspendable intermitter;
        protected IMainModel mainViewModel;

        private string GetStatistics(Stopwatch timer, int visitedVertices, IGraphPath path = null)
        {
            var format = ViewModelResources.StatisticsFormat;
            var pathLength = path?.GetPathLength() ?? 0;
            var pathCost = path?.GetPathCost() ?? 0;
            var graphInfo = string.Format(format, pathLength, pathCost, visitedVertices);
            var timerInfo = timer.GetTimeInformation(ViewModelResources.TimerInfoFormat);
            return string.Join(Separator, AlgorithmKey, timerInfo, graphInfo);
        }

        private const string Separator = "   ";
        private readonly Stopwatch timer;
        private int visitedVerticesCount;
    }
}
