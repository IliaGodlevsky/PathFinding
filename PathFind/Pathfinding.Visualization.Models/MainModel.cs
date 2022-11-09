using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Interface.Extensions;
using Pathfinding.GraphLib.Serialization.Core.Interface;
using Pathfinding.GraphLib.Serialization.Core.Realizations.Extensions;
using Pathfinding.Logging.Interface;
using Pathfinding.Visualization.Core.Abstractions;
using Pathfinding.Visualization.Extensions;
using Pathfinding.Visualization.Models.Interfaces;
using Pathfinding.VisualizationLib.Core.Interface;
using System;

namespace Pathfinding.Visualization.Models
{
    public abstract class MainModel<TGraph, TVertex, TField> : IMainModel<TGraph, TVertex, TField>
        where TGraph : IGraph<TVertex>
        where TVertex : IVertex, IVisualizable
        where TField : IGraphField<TVertex>
    {
        private readonly IGraphSubscription<TVertex> events;

        protected readonly ILog log;
        protected readonly IGraphFieldFactory<TGraph, TVertex, TField> fieldFactory;
        protected readonly VisualPathfindingRange<TVertex> pathfindingRange;
        protected readonly IGraphSerializationModule<TGraph, TVertex> serializationModule;

        public virtual string GraphParametres { get; set; }

        public virtual TField GraphField { get; set; }

        public virtual TGraph Graph { get; protected set; }

        protected MainModel(IGraphFieldFactory<TGraph, TVertex, TField> fieldFactory,
            IGraphSubscription<TVertex> events,
            IGraphSerializationModule<TGraph, TVertex> serializationModule,
            VisualPathfindingRange<TVertex> endPoints,
            ILog log)
        {
            this.events = events;
            this.serializationModule = serializationModule;
            this.fieldFactory = fieldFactory;
            this.pathfindingRange = endPoints;
            this.log = log;
        }

        public virtual async void SaveGraph()
        {
            try
            {
                await serializationModule.SaveGraphAsync((IGraph<IVertex>)Graph);
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
            Graph.RestoreVerticesVisualState();
            GraphParametres = Graph.GetStringRepresentation();
            pathfindingRange.Undo();
        }

        public virtual void ConnectNewGraph(TGraph graph)
        {
            pathfindingRange.Undo();
            events.Unsubscribe(Graph);
            Graph = graph;
            GraphField = fieldFactory.CreateGraphField(Graph);
            events.Subscribe(Graph);
            GraphParametres = Graph.GetStringRepresentation();
        }

        public void ClearColors()
        {
            Graph.RestoreVerticesVisualState();
            pathfindingRange.RestoreVerticesVisualState();
        }
    }
}