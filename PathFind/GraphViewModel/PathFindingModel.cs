using Algorithm.AlgorithmCreating;
using Algorithm.EventArguments;
using Algorithm.Intermitting;
using Algorithm.Intermitting.Interface;
using Common.Extensions;
using GraphLib.Extensions;
using GraphLib.Graphs.Infrastructure;
using GraphViewModel.Interfaces;
using GraphViewModel.Resources;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace GraphLib.ViewModel
{
    public abstract class PathFindingModel : IModel
    {
        public event Action<string> OnPathNotFound;

        public int DelayTime { get; set; } // milliseconds

        public string AlgorithmKey { get; set; }

        public virtual IList<string> AlgorithmKeys { get; set; }

        public PathFindingModel(IMainModel mainViewModel)
        {            
            this.mainViewModel = mainViewModel;
            DelayTime = 4;
        }

        public virtual void FindPath()
        {
            var algorithm = AlgorithmFactory.
                CreateAlgorithm(AlgorithmKey);

            algorithm.Graph = mainViewModel.Graph;

            intermitter = new AlgorithmIntermit(DelayTime);

            algorithm.OnVertexEnqueued += OnVertexEnqueued;
            algorithm.OnVertexVisited += OnVertexVisited;
            algorithm.OnFinished += OnAlgorithmFinished;
            algorithm.OnStarted += OnAlgorithmStarted;

            algorithm.FindPath();
            algorithm.Reset();

            OnPathNotFound = null;
        }

        protected abstract void OnAlgorithmIntermitted();

        protected virtual void OnVertexVisited(object sender, EventArgs e)
        {
            if (e is AlgorithmEventArgs args)
            {
                if (args.Vertex.IsSimpleVertex())
                {
                    args.Vertex.MarkAsVisited();
                }

                mainViewModel.PathFindingStatistics = GetIntermediateStatistics(timer,
                    steps: 0, pathLength: 0, args.Graph.NumberOfVisitedVertices);

                intermitter.Intermit();
                OnAlgorithmIntermitted();
            }
        }

        protected virtual void OnVertexEnqueued(object sender, EventArgs e)
        {
            if (e is AlgorithmEventArgs args)
            {
                if (args.Vertex.IsSimpleVertex())
                {
                    args.Vertex.MarkAsEnqueued();
                }
            }
        }

        protected virtual void OnAlgorithmFinished(object sender, EventArgs e)
        {
            if (e is AlgorithmEventArgs args)
            {
                timer.Stop();

                var path = new GraphPath(args.Graph);

                mainViewModel.PathFindingStatistics = GetIntermediateStatistics(timer,
                    path.PathLength, path.PathCost, args.Graph.NumberOfVisitedVertices);

                if (path.IsExtracted)
                {
                    path.HighlightPath();
                }
                else
                {
                    OnPathNotFound?.Invoke("Couln't find path");
                }
            }
        }

        protected virtual void OnAlgorithmStarted(object sender, EventArgs e)
        {
            timer = new Stopwatch();
            timer.Start();
        }

        private string GetIntermediateStatistics(Stopwatch timer,
            int steps, int pathLength, int visitedVertices)
        {
            var graphInfo = string.Format(ViewModelResources.StatisticsFormat,
                steps, pathLength, visitedVertices);

            var timerInfo = timer.GetTimeInformation(ViewModelResources.TimerInfoFormat);

            return AlgorithmKey + "   " + timerInfo + "   " + graphInfo;
        }

        protected IIntermit intermitter;
        protected IMainModel mainViewModel;

        private Stopwatch timer;
    }
}
