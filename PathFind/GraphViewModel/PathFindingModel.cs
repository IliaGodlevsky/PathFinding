using Algorithm.Common;
using Algorithm.Common.Exceptions;
using Algorithm.Extensions;
using Algorithm.Infrastructure.EventArguments;
using Algorithm.Interfaces;
using Algorithm.Realizations;
using Algorithm.Realizations.Enums;
using Common;
using Common.Extensions;
using Common.Interface;
using GraphLib.Base;
using GraphLib.Interfaces;
using GraphViewModel.Interfaces;
using Interruptable.EventArguments;
using Logging.Interface;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

using static GraphViewModel.Resources.ViewModelResources;

namespace GraphViewModel
{
    public abstract class PathFindingModel : IModel
    {
        public int DelayTime { get; set; }

        public Algorithms Algorithm { get; set; }

        public virtual IDictionary<string, Algorithms> Algorithms => algorithms.Value;

        protected PathFindingModel(ILog log, IMainModel mainViewModel, BaseEndPoints endPoints)
        {
            algorithms = new Lazy<IDictionary<string, Algorithms>>(GetAlgorithmsDictinary);
            this.mainViewModel = mainViewModel;
            this.endPoints = endPoints;
            this.log = log;

            DelayTime = 4;
            graph = mainViewModel.Graph;

            timer = new Stopwatch();
            vertexMark = new VertexMark();
            path = new NullGraphPath();
            algorithm = new NullAlgorithm();
        }

        public virtual void FindPath()
        {
            try
            {
                algorithm = AlgorithmFactory
                    .CreateAlgorithm(Algorithm, graph, endPoints);
                SubscribeOnAlgorithmEvents();
                path = algorithm.FindPath();
                Summarize();
            }
            catch (AlgorithmInterruptedException ex)
            {
                log.Warn(ex);
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
            finally
            {
                algorithm.Dispose();
                endPoints.Reset();
            }
        }

        protected abstract void ColorizeProcessedVertices(object sender, AlgorithmEventArgs e);

        protected virtual void OnVertexVisited(object sender, AlgorithmEventArgs e)
        {
            visitedVerticesCount = e.VisitedVertices;
            mainViewModel.PathFindingStatistics = GetStatistics();
        }

        protected virtual void OnAlgorithmInterrupted(object sender, InterruptEventArgs e)
        {
            throw new AlgorithmInterruptedException(AlgorithmInterruptedMsg);
        }

        protected virtual void OnAlgorithmFinished(object sender, AlgorithmEventArgs e)
        {
            visitedVerticesCount = e.VisitedVertices;
        }

        protected virtual void Summarize()
        {
            if (path.PathLength > 0)
            {
                path.Highlight(endPoints);
                mainViewModel.PathFindingStatistics = GetStatistics();
            }
            else
            {
                log.Warn(PathWasNotFoundMsg);
            }
        }

        protected virtual void OnAlgorithmStarted(object sender, AlgorithmEventArgs e)
        {
            interrupter = new Interrupter(DelayTime);
        }

        protected readonly BaseEndPoints endPoints;
        protected readonly IMainModel mainViewModel;
        protected readonly ILog log;
        protected ISuspendable interrupter;
        protected IAlgorithm algorithm;
        protected IGraphPath path;

        private string GetStatistics()
        {
            string timerInfo = timer.GetTimeInformation(TimerInfoFormat);
            string graphInfo = string.Format(StatisticsFormat,
                path.PathLength, path.PathCost, visitedVerticesCount);
            return string.Join(Separator, Algorithm.GetDescription(), timerInfo, graphInfo);
        }

        private void SubscribeOnAlgorithmEvents()
        {
            algorithm.OnVertexEnqueued += vertexMark.OnVertexEnqueued;
            algorithm.OnVertexVisited += OnVertexVisited;
            algorithm.OnVertexVisited += (s, e) => interrupter.Suspend();
            algorithm.OnVertexVisited += ColorizeProcessedVertices;
            algorithm.OnVertexVisited += vertexMark.OnVertexVisited;
            algorithm.OnFinished += OnAlgorithmFinished;
            algorithm.OnFinished += (s, e) => timer.Stop();
            algorithm.OnStarted += OnAlgorithmStarted;
            algorithm.OnStarted += (s, e) => timer.Reset();
            algorithm.OnStarted += (s, e) => timer.Start();
            algorithm.OnInterrupted += (s, e) => timer.Stop();
            algorithm.OnInterrupted += OnAlgorithmInterrupted;
        }

        private IDictionary<string, Algorithms> GetAlgorithmsDictinary()
        {
            return Enum
                .GetValues(typeof(Algorithms))
                .Cast<Algorithms>()
                .ToDictionary(item => ((Enum)item).GetDescription());
        }

        private const string Separator = "   ";
        private readonly Lazy<IDictionary<string, Algorithms>> algorithms;
        private readonly VertexMark vertexMark;
        private readonly IGraph graph;
        private readonly Stopwatch timer;
        private int visitedVerticesCount;
    }
}