using AssembleClassesLib.Interface;
using Common.Extensions;
using Common.Logging;
using GraphLib.Base;
using GraphLib.Common.NullObjects;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using GraphLib.Interfaces.Factories;
using GraphLib.Serialization.Interfaces;
using GraphViewModel.Interfaces;
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
            IAssembleClasses assembleClasses)
        {
            this.eventHolder = eventHolder;
            serializer = graphSerializer;
            graphSerializer.OnExceptionCaught += (ex, msg) =>
            {
                OnExceptionCaught(ex, msg);
                Logger.Instance.Warn(ex);
            };
            this.fieldFactory = fieldFactory;
            this.graphAssembler = graphAssembler;
            this.pathInput = pathInput;
            this.assembleClasses = assembleClasses;

            Graph = new NullGraph();
        }

        public virtual async void SaveGraph()
        {
            string savePath = pathInput.InputSavePath();

            try
            {
                using (var stream = new FileStream(savePath, FileMode.OpenOrCreate))
                {
                    await serializer.SaveGraphAsync(Graph, stream);
                }
            }
            catch (Exception ex)
            {
                OnExceptionCaught(ex);
                Logger.Instance.Warn(ex);
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
                OnExceptionCaught(ex);
                Logger.Instance.Warn(ex);
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
        protected abstract void OnExceptionCaught(Exception ex, string additaionalMessage = "");

        protected readonly IGraphAssemble graphAssembler;
        protected readonly BaseGraphFieldFactory fieldFactory;
        private readonly IVertexEventHolder eventHolder;
        private readonly IGraphSerializer serializer;
        private readonly IPathInput pathInput;
        protected readonly IAssembleClasses assembleClasses;
    }
}
