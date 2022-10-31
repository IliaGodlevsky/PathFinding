using GraphLib.Base.EndPoints;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using GraphLib.Serialization.Extensions;
using GraphLib.Serialization.Interfaces;
using GraphViewModel.Interfaces;
using Logging.Interface;
using System;

namespace GraphViewModel
{
    public abstract class MainModel<TGraph, TVertex, TField> : IMainModel<TGraph, TVertex, TField>
        where TGraph : IGraph<TVertex>
        where TVertex : IVertex, IVisualizable
        where TField : IGraphField<TVertex>
    {
        private readonly IGraphEvents<TVertex> events;

        protected readonly ILog log;
        protected readonly IGraphFieldFactory<TGraph, TVertex, TField> fieldFactory;
        protected readonly BaseEndPoints<TVertex> endPoints;
        protected readonly IGraphSerializationModule<TGraph, TVertex> serializationModule;

        public virtual string GraphParametres { get; set; }

        public virtual TField GraphField { get; set; }

        public virtual TGraph Graph { get; protected set; }

        protected MainModel(IGraphFieldFactory<TGraph, TVertex, TField> fieldFactory,
            IGraphEvents<TVertex> events,
            IGraphSerializationModule<TGraph, TVertex> serializationModule,
            BaseEndPoints<TVertex> endPoints,
            ILog log)
        {
            this.events = events;
            this.serializationModule = serializationModule;
            this.fieldFactory = fieldFactory;
            this.endPoints = endPoints;
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
            Graph.Refresh();
            GraphParametres = Graph.GetStringRepresentation();
            endPoints.Reset();
        }

        public virtual void ConnectNewGraph(TGraph graph)
        {
            endPoints.Reset();
            events.Unsubscribe(Graph);
            Graph = graph;
            GraphField = fieldFactory.CreateGraphField(Graph);
            events.Subscribe(Graph);
            GraphParametres = Graph.GetStringRepresentation();
        }

        public void ClearColors()
        {
            Graph.Refresh();
            endPoints.RestoreCurrentColors();
        }
    }
}