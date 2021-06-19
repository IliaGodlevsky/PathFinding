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
            this.mainViewModel = mainViewModel;
            this.endPoints = endPoints;
            this.log = log;

            AlgorithmKeys = algorithms.ClassesNames.ToList();
            DelayTime = 4;
            graph = mainViewModel.Graph;
            assembleClasses = algorithms;  
            
            keysUpdate = new AlgorithmsKeysUpdate(this);
            timer = new Stopwatch();
            vertexMark = new VertexMark();
            path = new NullGraphPath();
            algorithm = new NullAlgorithm();
        }

        public virtual void FindPath()
        {
            try
            {
                algorithm = (IAlgorithm)assembleClasses
                    .Get(AlgorithmKey, graph, endPoints);
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
            keysUpdate.UpdateAlgorithmKeys(sender, e);
        }

        protected abstract void ColorizeProcessedVertices(object sender, EventArgs e);

        protected virtual void OnVertexVisited(object sender, EventArgs e)
        {
            if (e is AlgorithmEventArgs args)
            {
                visitedVerticesCount = args.VisitedVertices;
                mainViewModel.PathFindingStatistics = GetStatistics();
            }
        }

        protected virtual void OnAlgorithmInterrupted(object sender, EventArgs e)
        {
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
        }

        protected virtual void Summarize()
        {
            if (path.HasPath())
            {
                path.Highlight(endPoints);
                mainViewModel.PathFindingStatistics = GetStatistics();
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
        }

        private string GetStatistics()
        {
            string timerInfo = timer.GetTimeInformation(TimerInfoFormat);
            string graphInfo = string.Format(StatisticsFormat,
                path.PathLength, path.PathCost, visitedVerticesCount);
            return string.Join(Separator, AlgorithmKey, timerInfo, graphInfo);
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

        private const string Separator = "   ";

        protected readonly BaseEndPoints endPoints;
        protected readonly IMainModel mainViewModel;
        protected readonly ILog log;
        protected readonly IAssembleClasses assembleClasses;
        private readonly VertexMark vertexMark;
        private readonly IGraph graph;
        private readonly AlgorithmsKeysUpdate keysUpdate;

        protected ISuspendable interrupter;
        protected IAlgorithm algorithm;
        protected IGraphPath path;
        private int visitedVerticesCount;
        private readonly Stopwatch timer;
    }
}