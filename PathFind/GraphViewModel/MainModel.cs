using AssembleClassesLib.Interface;
using GraphLib.Base;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using GraphLib.NullRealizations.NullObjects;
using GraphViewModel.Interfaces;
using Logging.Interface;
using NullObject.Extensions;
using System;

namespace GraphViewModel
{
    public abstract class MainModel : IMainModel
    {
        public BaseEndPoints EndPoints { get; set; }

        public virtual string GraphParametres { get; set; }

        public virtual string PathFindingStatistics { get; set; }

        public virtual IGraphField GraphField { get; set; }

        public virtual IGraph Graph { get; protected set; }

        protected MainModel(BaseGraphFieldFactory fieldFactory,
            IVertexEventHolder eventHolder,
            ISaveLoadGraph saveLoad,
            IAssembleClasses graphFactories,
            IAssembleClasses assembleClasses,
            ILog log)
        {
            this.eventHolder = eventHolder;
            this.saveLoad = saveLoad;
            this.fieldFactory = fieldFactory;
            this.graphFactories = graphFactories;
            this.assembleClasses = assembleClasses;
            this.log = log;

            this.graphFactories.LoadClasses();

            Graph = new NullGraph();
        }

        public virtual async void SaveGraph()
        {
            try
            {
                await saveLoad.SaveGraphAsync(Graph);
            }
            catch (Exception ex)
            {
                log.Warn(ex);
            }

        }

        public virtual void LoadGraph()
        {
            try
            {
                var newGraph = saveLoad.LoadGraph();
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
            PathFindingStatistics = string.Empty;
            GraphParametres = Graph.ToString();
            EndPoints.Reset();
        }

        public void ConnectNewGraph(IGraph graph)
        {
            if (!graph.IsNullObject())
            {
                EndPoints.UnsubscribeFromEvents(Graph);
                EndPoints.Reset();
                eventHolder.UnsubscribeVertices(Graph);
                Graph = graph;
                GraphField = fieldFactory.CreateGraphField(Graph);
                eventHolder.SubscribeVertices(Graph);
                EndPoints.SubscribeToEvents(Graph);
                GraphParametres = Graph.ToString();
                PathFindingStatistics = string.Empty;
            }
        }

        protected readonly IAssembleClasses graphFactories;
        protected readonly BaseGraphFieldFactory fieldFactory;
        protected readonly IAssembleClasses assembleClasses;
        protected readonly ILog log;

        private readonly IVertexEventHolder eventHolder;
        private readonly ISaveLoadGraph saveLoad;
    }
}
