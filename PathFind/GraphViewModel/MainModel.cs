using Algorithm.Factory;
using GraphLib.Base;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using GraphLib.Interfaces.Factories;
using GraphLib.NullRealizations.NullObjects;
using GraphLib.Serialization;
using GraphLib.Serialization.Extensions;
using GraphViewModel.Interfaces;
using Logging.Interface;
using System;
using System.Collections.Generic;

namespace GraphViewModel
{
    public abstract class MainModel : IMainModel
    {
        public virtual string GraphParametres { get; set; }

        public virtual IGraphField GraphField { get; set; }

        public virtual IGraph Graph { get; protected set; }

        protected MainModel(IGraphFieldFactory fieldFactory,
            IVertexEventHolder eventHolder,
            GraphSerializationModule serializationModule,
            IEnumerable<IGraphAssemble> graphAssembles,
            BaseEndPoints endPoints,
            IEnumerable<IAlgorithmFactory> algorithmFactories,
            ILog log)
        {
            this.eventHolder = eventHolder;
            this.serializationModule = serializationModule;
            this.fieldFactory = fieldFactory;
            this.graphAssembles = graphAssembles;
            this.endPoints = endPoints;
            this.log = log;
            this.algorithmFactories = algorithmFactories;
            Graph = NullGraph.Instance;
        }

        public virtual async void SaveGraph()
        {
            var task = serializationModule.SaveGraphAsync(Graph);
            try
            {
                await task;
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
            GraphParametres = Graph.ToString();
            endPoints.Reset();
        }

        public virtual void ConnectNewGraph(IGraph graph)
        {
            endPoints.UnsubscribeFromEvents(Graph);
            endPoints.Reset();
            eventHolder.UnsubscribeVertices(Graph);
            Graph = graph;
            GraphField = fieldFactory.CreateGraphField(Graph);
            endPoints.SubscribeToEvents(Graph);
            eventHolder.SubscribeVertices(Graph);
            GraphParametres = Graph.ToString();
        }

        public void ClearColors()
        {
            Graph.Refresh();
            endPoints.ReturnColors();
        }

        protected readonly IEnumerable<IGraphAssemble> graphAssembles;
        protected readonly IGraphFieldFactory fieldFactory;
        protected readonly ILog log;
        protected readonly BaseEndPoints endPoints;
        protected readonly GraphSerializationModule serializationModule;
        protected readonly IEnumerable<IAlgorithmFactory> algorithmFactories;

        private readonly IVertexEventHolder eventHolder;
    }
}
