using Common;
using Common.Extensions;
using GraphLib.Base;
using GraphLib.Common.NullObjects;
using GraphLib.Extensions;
using GraphLib.Interface;
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

        public MainModel(BaseGraphFieldFactory fieldFactory,
            IVertexEventHolder eventHolder,
            IGraphSerializer graphSerializer,
            IGraphAssembler graphAssembler,
            IPathInput pathInput)
        {          
            this.eventHolder = eventHolder;
            serializer = graphSerializer;
            this.fieldFactory = fieldFactory;
            this.graphAssembler = graphAssembler;
            this.pathInput = pathInput;

            Graph = new NullGraph();
        }

        public async virtual void SaveGraph()
        {
            var savePath = pathInput.InputSavePath();
            try
            {
                using (var stream = new FileStream(savePath, FileMode.OpenOrCreate))
                {
                    await serializer.SaveGraphAsync(Graph, stream);
                }
            }
            catch (Exception ex)
            {
                Logger.Instance.Error(ex);
            }
        }

        public virtual void LoadGraph()
        {
            var loadPath = pathInput.InputLoadPath();
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
                Logger.Instance.Error(ex);
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
            if (!graph.IsDefault())
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

        protected abstract string GetAlgorithmsLoadPath();

        protected readonly IGraphAssembler graphAssembler;
        private readonly IVertexEventHolder eventHolder;
        private readonly IGraphSerializer serializer;
        private readonly BaseGraphFieldFactory fieldFactory;
        private readonly IPathInput pathInput;
    }
}
