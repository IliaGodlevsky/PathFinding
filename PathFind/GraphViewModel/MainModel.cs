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
        private readonly IVertexEventHolder eventHolder;

        protected readonly ILog log;
        protected readonly IGraphFieldFactory fieldFactory;
        protected readonly BaseEndPoints endPoints;
        protected readonly IGraphSerializationModule serializationModule;

        public virtual string GraphParametres { get; set; }

        public virtual IGraphField GraphField { get; set; }

        public virtual IGraph Graph { get; protected set; }

        protected MainModel(IGraphFieldFactory fieldFactory,
            IVertexEventHolder eventHolder,
            IGraphSerializationModule serializationModule,
            BaseEndPoints endPoints,
            ILog log)
        {
            this.eventHolder = eventHolder;
            this.serializationModule = serializationModule;
            this.fieldFactory = fieldFactory;
            this.endPoints = endPoints;
            this.log = log;
            Graph = NullGraph.Instance;
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
            endPoints.RestoreCurrentColors();
        }
    }
}