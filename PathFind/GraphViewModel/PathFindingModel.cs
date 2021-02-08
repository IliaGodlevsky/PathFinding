using Algorithm;
using Algorithm.EventArguments;
using Algorithm.Interface;
using Common;
using Common.Extensions;
using GraphLib.Extensions;
using GraphLib.Infrastructure;
using GraphLib.Interface;
using GraphViewModel.Interfaces;
using GraphViewModel.Resources;
using System;
using System.Collections.Generic;
using System.Diagnostics;

using static Algorithm.AlgorithmCreating.AlgorithmFactory;

namespace GraphLib.ViewModel
{
    public abstract class PathFindingModel : IModel
    {
        public event Action<string> OnPathNotFound;

        public ValueRange AlgorithmDelayTimeValueRange { get; protected set; }

        public int DelayTime { get; set; } // milliseconds

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
                intermitter = new AlgorithmIntermit(DelayTime);
                intermitter.OnIntermitted += OnAlgorithmIntermitted;

                algorithm.OnVertexEnqueued += OnVertexEnqueued;
                algorithm.OnVertexVisited += OnVertexVisited;
                algorithm.OnFinished += OnAlgorithmFinished;
                algorithm.OnStarted += OnAlgorithmStarted;

                var start = mainViewModel.Graph.Start;
                var end = mainViewModel.Graph.End;
                var path = algorithm.FindPath(start, end);

                var finishStatistics = GetIntermediateStatistics(timer, 0, path);
                mainViewModel.PathFindingStatistics = finishStatistics;

                if (path.IsExtracted)
                {
                    path.HighlightPath();
                }
                else
                {
                    OnPathNotFound?.Invoke("Couln't find path");
                }

                algorithm.Reset();

                OnPathNotFound = null;
            }
            catch (Exception ex)
            {
                Logger.Instance.Error(ex);
            }
        }

        protected abstract void OnAlgorithmIntermitted();

        protected virtual void OnVertexVisited(object sender, EventArgs e)
        {
            if (e is AlgorithmEventArgs args)
            {
                if (!args.IsExtremeVertex)
                {
                    args.Vertex.MarkAsVisited();
                }
                var intermediateStatistics = GetIntermediateStatistics(timer, args.VisitedVertices);
                mainViewModel.PathFindingStatistics = intermediateStatistics;
            }

            intermitter.Intermit();
        }

        protected virtual void OnVertexEnqueued(object sender, EventArgs e)
        {
            if (e is AlgorithmEventArgs args)
            {
                if (!args.IsExtremeVertex)
                {
                    args.Vertex.MarkAsEnqueued();
                }
            }
        }

        protected virtual void OnAlgorithmFinished(object sender, EventArgs e)
        {
            timer.Stop();

            timer.Reset();
        }

        protected virtual void OnAlgorithmStarted(object sender, EventArgs e)
        {
            timer.Start();
        }

        private string GetIntermediateStatistics(Stopwatch timer, 
            int visitedCount, GraphPath path = null)
        {
            var format = ViewModelResources.StatisticsFormat;
            var pathLength = path == null ? 0 : path.Length;
            var pathCost = path == null ? 0 : path.Cost;
            var graphInfo = string.Format(format, pathLength, pathCost, visitedCount);
            var timerInfo = timer.GetTimeInformation(ViewModelResources.TimerInfoFormat);

            return $"{AlgorithmKey}    {timerInfo}     {graphInfo}";
        }

        protected IIntermit intermitter;
        protected IMainModel mainViewModel;

        private readonly Stopwatch timer;
    }
}
