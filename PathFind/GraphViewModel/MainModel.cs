using AssembleClassesLib.Interface;
using GraphLib.Base;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using GraphLib.Interfaces.Factories;
using GraphLib.NullRealizations.NullObjects;
using GraphViewModel.Interfaces;
using Logging.Interface;
using System;
using System.Collections.Generic;

namespace GraphViewModel
{
    public abstract class MainModel : IMainModel
    {
        public BaseEndPoints EndPoints { get; set; }

        public virtual string GraphParametres { get; set; }

        public virtual string PathFindingStatistics { get; set; }

        public virtual IGraphField GraphField { get; set; }

        public virtual IGraph Graph { get; protected set; }

        protected MainModel(IGraphFieldFactory fieldFactory,
            IVertexEventHolder eventHolder,
            ISaveLoadGraph saveLoad,
            IEnumerable<IGraphAssemble> graphAssembles,
            IAssembleClasses algorithmClasses,
            ILog log)
        {
            this.eventHolder = eventHolder;
            this.saveLoad = saveLoad;
            this.fieldFactory = fieldFactory;
            this.graphAssembles = graphAssembles;
            this.algorithmClasses = algorithmClasses;
            this.log = log;

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

        protected readonly IEnumerable<IGraphAssemble> graphAssembles;
        protected readonly IGraphFieldFactory fieldFactory;
        protected readonly IAssembleClasses algorithmClasses;
        protected readonly ILog log;

        private readonly IVertexEventHolder eventHolder;
        private readonly ISaveLoadGraph saveLoad;
    }
}
