using GalaSoft.MvvmLight.Messaging;
using Pathfinding.AlgorithmLib.Core.Abstractions;
using Pathfinding.AlgorithmLib.Core.Interface;
using Pathfinding.App.Console.DataAccess.Models;
using Pathfinding.App.Console.DataAccess.UnitOfWorks;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Messages;
using Pathfinding.App.Console.Model;
using Pathfinding.App.Console.Serialization;
using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Interface.Extensions;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using Pathfinding.Logging.Interface;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.App.Console.Units
{
    internal sealed class StorageUnit : Unit, ICanRecieveMessage
    {
        private readonly IMessenger messenger;
        private readonly IUnitOfWork unitOfWork;
        private readonly ILog log;

        private GraphModel CurrentGraph { get; set; } = new();

        private AlgorithmModel CurrentAlgorithm { get; set; }

        public StorageUnit(IReadOnlyCollection<IMenuItem> menuItems, 
            IReadOnlyCollection<IConditionedMenuItem> conditioned,
            IMessenger messenger, IUnitOfWork unitOfWork, ILog log) 
            : base(menuItems, conditioned)
        {
            this.messenger = messenger;
            this.unitOfWork = unitOfWork;
            this.log = log;
        }

        private void AddGraph(Graph2D<Vertex> graph)
        {
            CurrentGraph = unitOfWork.AddGraph(graph);
            unitOfWork.AddGraphInformation(CurrentGraph.Id, graph.ToString());
        }

        private void AddAlgorithm(PathfindingProcess process)
        {
            CurrentAlgorithm = unitOfWork.AddAlgorithm(CurrentGraph.Id, process.ToString());
        }

        private void SetGraph(long graphId)
        {
            CurrentGraph = unitOfWork.GraphRepository.GetById(graphId);
            messenger.SendData(CurrentGraph.Graph, Tokens.Common);
        }

        private void AddVisited(VisitedMessage msg)
        {
            unitOfWork.AddVisited(CurrentAlgorithm.Id, msg.Coordinates);
        }

        private void AddPath(IGraphPath path)
        {
            unitOfWork.AddPath(CurrentAlgorithm.Id, path);
        }

        private void AddObstacles(ObstaclesMessage msg)
        {
            unitOfWork.AddObstacles(CurrentAlgorithm.Id, msg.Coordinates);
        }

        private void AddRange(RangeMessage msg)
        {
            unitOfWork.AddRange(CurrentAlgorithm.Id, msg.Coordinates);
        }
        
        private void AddStatistics(StatisticsModel model)
        {
            unitOfWork.AddStatistics(CurrentAlgorithm.Id, model);
        }

        private void AddCosts(IReadOnlyList<int> costs)
        {
            unitOfWork.AddCosts(CurrentAlgorithm.Id, costs);
        }

        private void UpdatePathfindingRange(IPathfindingRange<Vertex> range)
        {
            CurrentGraph.Range = range.GetCoordinates().ToArray();
            CurrentGraph = unitOfWork.UpdateGraph(CurrentGraph);
        }

        private void UpdateGraph(UpdateGraphMessage msg)
        {
            CurrentGraph = unitOfWork.UpdateGraph(CurrentGraph);
        }

        private void SendSerializationInfo(AskSerializationInfoMessage msg)
        {
            msg.Response = unitOfWork.GetSerializationInfo(CurrentGraph.Id);
        }

        private void SetSerializationInfo(SerializationInfo info)
        {
            unitOfWork.AddSerializationInfo(info);
        }

        private void SendStatistics(AskStatisticsMessage msg)
        {
            var algorithms = unitOfWork.AlgorithmRepository
                .GetAll(a => a.GraphId == CurrentGraph.Id)
                .Select(a => a.Id)
                .ToList();
            var statistics = unitOfWork.StatisticsRepository
                .GetAll(s => algorithms.Contains(s.AlgorithmId))
                .ToList();
            msg.Response = statistics;
        }

        private void SendVisualizationSet(AskVisualizationSetMessage msg)
        {
            msg.Response = unitOfWork.GetVisualizationSet(msg.Id);
        }

        public void RegisterHanlders(IMessenger messenger)
        {
            messenger.RegisterGraph(this, Tokens.Storage, AddGraph);
            messenger.RegisterData<PathfindingProcess>(this, Tokens.Storage, AddAlgorithm);
            messenger.RegisterData<VisitedMessage>(this, Tokens.Storage, AddVisited);
            messenger.RegisterData<IGraphPath>(this, Tokens.Storage, AddPath);
            messenger.RegisterData<ObstaclesMessage>(this, Tokens.Storage, AddObstacles);
            messenger.RegisterData<RangeMessage>(this, Tokens.Storage, AddRange);
            messenger.RegisterData<StatisticsModel>(this, Tokens.Storage, AddStatistics);
            messenger.RegisterData<IPathfindingRange<Vertex>>(this, Tokens.Storage, UpdatePathfindingRange);
            messenger.RegisterData<IReadOnlyList<int>>(this, Tokens.Storage, AddCosts);
            messenger.RegisterData<long>(this, Tokens.Storage, SetGraph);
            messenger.Register<UpdateGraphMessage>(this, Tokens.Storage, UpdateGraph);
            messenger.RegisterData<SerializationInfo>(this, Tokens.Storage, SetSerializationInfo);
            messenger.Register<AskSerializationInfoMessage>(this, Tokens.Storage, SendSerializationInfo);
            messenger.Register<AskStatisticsMessage>(this, Tokens.Storage, SendStatistics);
            messenger.Register<AskVisualizationSetMessage>(this, Tokens.Storage, SendVisualizationSet);
        }
    }
}
