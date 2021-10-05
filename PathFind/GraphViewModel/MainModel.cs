using Common.Extensions;
using GraphLib.Base;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using GraphLib.Interfaces.Factories;
using GraphLib.NullRealizations.NullObjects;
using GraphViewModel.Interfaces;
using Logging.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphViewModel
{
    public abstract class MainModel : IMainModel
    {
        public virtual string GraphParametres { get; set; }

        public virtual IGraphField GraphField { get; set; }

        public virtual IGraph Graph { get; protected set; }

        protected MainModel(IGraphFieldFactory fieldFactory,
            IVertexEventHolder eventHolder,
            ISaveLoadGraph saveLoad,
            IEnumerable<IGraphAssemble> graphAssembles,
            BaseEndPoints endPoints,
            ILog log)
        {
            this.eventHolder = eventHolder;
            this.saveLoad = saveLoad;
            this.fieldFactory = fieldFactory;
            this.graphAssembles = graphAssembles;
            this.endPoints = endPoints;
            this.log = log;
            Graph = new NullGraph();
        }

        public virtual async void SaveGraph()
        {
            var task = saveLoad.SaveGraphAsync(Graph);
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
                var newGraph = await saveLoad.LoadGraphAsync();
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
            ClearColors();
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
            Graph.Vertices
                .OfType<IVisualizable>()
                .Where(vertex => !vertex.IsVisualizedAsEndPoint)
                .ForEach(vertex => vertex.VisualizeAsRegular());
        }

        protected readonly IEnumerable<IGraphAssemble> graphAssembles;
        protected readonly IGraphFieldFactory fieldFactory;
        protected readonly ILog log;
        protected readonly BaseEndPoints endPoints;
        protected readonly ISaveLoadGraph saveLoad;

        private readonly IVertexEventHolder eventHolder;
    }
}
