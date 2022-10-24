using GraphLib.Base.EndPoints;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using GraphLib.NullRealizations;
using GraphLib.Serialization.Extensions;
using GraphLib.Serialization.Interfaces;
using GraphViewModel.Interfaces;
using Logging.Interface;
using System;

namespace GraphViewModel
{
    public abstract class MainModel : IMainModel
    {
        private readonly IGraphEvents events;

        protected readonly ILog log;
        protected readonly IGraphFieldFactory fieldFactory;
        protected readonly BasePathfindingRange pathfindingRange;
        protected readonly IGraphSerializationModule serializationModule;

        public virtual string GraphParametres { get; set; }

        public virtual IGraphField GraphField { get; set; }

        public virtual IGraph Graph { get; protected set; }

        protected MainModel(IGraphFieldFactory fieldFactory,
            IGraphEvents events,
            IGraphSerializationModule serializationModule,
            BasePathfindingRange endPoints,
            ILog log)
        {
            this.events = events;
            this.serializationModule = serializationModule;
            this.fieldFactory = fieldFactory;
            this.pathfindingRange = endPoints;
            this.log = log;
            Graph = NullGraph.Interface;
        }

        public virtual async void SaveGraph()
        {
            try
            {
                await serializationModule.SaveGraphAsync(Graph);
            }
            catch (Exception ex)
            {
                log.Warn(ex);
            }
        }

        public virtual async void LoadGraph()
        {
            try
            {
                var newGraph = await serializationModule.LoadGraphAsync();
                ConnectNewGraph(newGraph);
            }
            catch (Exception ex)
            {
                log.Warn(ex);
            }
        }

        public abstract void FindPath();

        public abstract void CreateNewGraph();

        public virtual void ClearGraph()
        {
            Graph.Refresh();
            GraphParametres = Graph.GetStringRepresentation();
            pathfindingRange.Reset();
        }

        public virtual void ConnectNewGraph(IGraph graph)
        {
            pathfindingRange.Reset();
            events.Unsubscribe(Graph);
            Graph = graph;
            GraphField = fieldFactory.CreateGraphField(Graph);
            events.Subscribe(Graph);
            GraphParametres = Graph.GetStringRepresentation();
        }

        public void ClearColors()
        {
            Graph.Refresh();
            pathfindingRange.RestoreCurrentColors();
        }
    }
}