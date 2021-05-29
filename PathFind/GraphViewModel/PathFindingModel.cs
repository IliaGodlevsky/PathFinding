using Algorithm.Common;
using Algorithm.Common.Exceptions;
using Algorithm.Extensions;
using Algorithm.Infrastructure.EventArguments;
using Algorithm.Interfaces;
using AssembleClassesLib.EventArguments;
using AssembleClassesLib.Interface;
using Common;
using Common.Extensions;
using Common.Interface;
using GraphLib.Base;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using GraphViewModel.Interfaces;
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

        public string AlgorithmKey { get; set; }

        public virtual IList<string> AlgorithmKeys { get; set; }

        protected PathFindingModel(ILog log, IAssembleClasses algorithms,
            IMainModel mainViewModel, BaseEndPoints endPoints)
        {
            AlgorithmKeys = algorithms.ClassesNames.ToList();
            this.mainViewModel = mainViewModel;
            DelayTime = 4;
            timer = new Stopwatch();
            path = new NullGraphPath();
            algorithm = new NullAlgorithm();
            graph = mainViewModel.Graph;
            assembleClasses = algorithms;
            this.endPoints = endPoints;
            this.log = log;
        }

        public virtual void FindPath()
        {
            try
            {
                algorithm = (IAlgorithm)assembleClasses.Get(AlgorithmKey, graph, endPoints);
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

        public virtual void UpdateAlgorithmKeys(object sender, AssembleClassesEventArgs e)
        {
            var currentLoadedPluginsKeys = e.ClassesNames.ToArray();
            var addedAlgorithms = currentLoadedPluginsKeys.Except(AlgorithmKeys).ToArray();
            var deletedAlgorithms = AlgorithmKeys.Except(currentLoadedPluginsKeys).ToArray();
            if (addedAlgorithms.Any())
            {
                AlgorithmKeys.AddRange(addedAlgorithms);
            }
            if (deletedAlgorithms.Any())
            {
                AlgorithmKeys.RemoveRange(deletedAlgorithms);
            }
            if (addedAlgorithms.Any() || deletedAlgorithms.Any())
            {
                AlgorithmKeys = AlgorithmKeys.OrderBy(key => key).ToList();
            }
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
                string statistics = GetStatistics();
                mainViewModel.PathFindingStatistics = statistics;
            }
            if (DelayTime > 0)
            {
                ColorizeProcessedVertices();
                interrupter.Suspend();
            }
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
            log.Info(message);
            throw new AlgorithmInterruptedException(AlgorithmInterruptedMsg);
        }

        protected virtual void OnAlgorithmFinished(object sender, EventArgs e)
        {
            if (e is AlgorithmEventArgs args)
            {
                visitedVerticesCount = args.VisitedVertices;
                string message = $"Algorithm { AlgorithmKey } was finished";
                log.Info(message);
            }

            timer.Stop();
        }

        protected virtual void Summarize()
        {
            if (path.HasPath())
            {
                path.Highlight(endPoints);
                string statistics = GetStatistics();
                mainViewModel.PathFindingStatistics = statistics;
            }
            else
            {
                log.Warn(PathWasNotFoundMsg);
            }
        }

        protected virtual void OnAlgorithmStarted(object sender, EventArgs e)
        {
            interrupter = new Interrupter(DelayTime);
            string message = $"Algorithm {AlgorithmKey} was started. ";
            message += $"Start vertex: {endPoints.Source.GetInforamtion()};";
            message += $"End vertex: {endPoints.Target.GetInforamtion()}";
            log.Info(message);
            timer.Reset();
            timer.Start();
        }

        protected readonly BaseEndPoints endPoints;
        protected ISuspendable interrupter;
        protected readonly IMainModel mainViewModel;
        protected readonly ILog log;
        protected IAlgorithm algorithm;
        protected IGraphPath path;
        protected readonly IAssembleClasses assembleClasses;

        private string GetStatistics()
        {
            int pathLength = path?.PathLength ?? 0;
            double pathCost = path?.PathCost ?? 0;
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
        private readonly IGraph graph;
    }
}
