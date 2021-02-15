using Algorithm.Extensions;
using Algorithm.Infrastructure.EventArguments;
using Algorithm.Interfaces;
using Algorithm.Realizations;
using Common;
using Common.Extensions;
using GraphLib.Base;
using GraphLib.Interface;
using GraphViewModel.Interfaces;
using GraphViewModel.Resources;
using System;
using System.Collections.Generic;
using System.Diagnostics;

using static Algorithm.Realizations.AlgorithmsPluginLoader;

namespace GraphLib.ViewModel
{
    public abstract class PathFindingModel : IModel
    {
        public event Action<string> OnPathNotFound;

        public BaseEndPoints EndPoints { get; set; }

        public ValueRange AlgorithmDelayTimeValueRange { get; protected set; }

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

                intermitter = new AlgorithmIntermit(DelayTime);
                intermitter.OnIntermitted += OnAlgorithmIntermitted;

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
                OnPathNotFound?.Invoke("Couln't find path");
                return false;
            }
        }

        protected abstract void OnAlgorithmIntermitted();

        protected virtual void OnVertexVisited(object sender, EventArgs e)
        {
            if (e is AlgorithmEventArgs args)
            {
                if (!args.IsEndPoint && args.Vertex is IMarkableVertex)
                {
                    (args.Vertex as IMarkableVertex).MarkAsVisited();
                }

                var statistics = GetStatistics(timer, args.VisitedVertices);
                mainViewModel.PathFindingStatistics = statistics;
            }

            intermitter.Intermit();
        }

        protected virtual void OnVertexEnqueued(object sender, EventArgs e)
        {
            if (e is AlgorithmEventArgs args)
            {
                if (!args.IsEndPoint && args.Vertex is IMarkableVertex)
                {
                    (args.Vertex as IMarkableVertex).MarkAsEnqueued();
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

        private string GetStatistics(Stopwatch timer, int visitedVertices, IGraphPath path = null)
        {
            var format = ViewModelResources.StatisticsFormat;
            var pathLength = path == null ? 0 : path.GetPathLength();
            var pathCost = path == null ? 0 : path.GetPathCost();
            var graphInfo = string.Format(format, pathLength, pathCost, visitedVertices);
            var timerInfo = timer.GetTimeInformation(ViewModelResources.TimerInfoFormat);

            return $"{AlgorithmKey}    {timerInfo}     {graphInfo}";
        }

        protected IIntermit intermitter;
        protected IMainModel mainViewModel;

        private readonly Stopwatch timer;
        private int visitedVerticesCount;
    }
}
