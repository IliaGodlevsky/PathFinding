using AssembleClassesLib.Interface;
using Common.Extensions;
using GraphLib.Base;
using GraphLib.Common.NullObjects;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using GraphLib.Interfaces.Factories;
using GraphLib.Serialization.Interfaces;
using GraphViewModel.Interfaces;
using Logging.Interface;
using System;
using System.IO;

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
            IGraphSerializer graphSerializer,
            IGraphAssemble graphAssembler,
            IPathInput pathInput,
            IAssembleClasses assembleClasses,
            ILog log)
        {
            this.eventHolder = eventHolder;
            serializer = graphSerializer;
            graphSerializer.OnExceptionCaught += log.Warn;
            this.fieldFactory = fieldFactory;
            this.graphAssembler = graphAssembler;
            this.pathInput = pathInput;
            this.assembleClasses = assembleClasses;
            this.log = log;

            Graph = new NullGraph();
        }

        public virtual async void SaveGraph()
        {
            string savePath = pathInput.InputSavePath();
            using (var stream = new FileStream(savePath, FileMode.OpenOrCreate))
            {
                await serializer.SaveGraphAsync(Graph, stream);
            }
        }

        public virtual void LoadGraph()
        {
            string loadPath = pathInput.InputLoadPath();
            try
            {
                using (var stream = new FileStream(loadPath, FileMode.Open))
                {
                    var newGraph = serializer.LoadGraph(stream);
                    ConnectNewGraph(newGraph);
                }
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

        protected abstract void OnExternalEventHappened(string message);

        protected readonly IGraphAssemble graphAssembler;
        protected readonly BaseGraphFieldFactory fieldFactory;
        protected readonly IAssembleClasses assembleClasses;
        protected readonly ILog log;

        private readonly IVertexEventHolder eventHolder;
        private readonly IGraphSerializer serializer;
        private readonly IPathInput pathInput;

    }
}
