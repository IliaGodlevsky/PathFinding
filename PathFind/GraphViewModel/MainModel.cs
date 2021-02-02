using GraphLib.Base;
using GraphLib.Extensions;
using GraphLib.Interface;
using GraphLib.NullObjects;
using GraphViewModel.Interfaces;
using GraphViewModel.Resources;
using System.IO;

namespace GraphViewModel
{
    public abstract class MainModel : IMainModel
    {
        public virtual string GraphParametres { get; set; }

        public virtual string PathFindingStatistics { get; set; }

        public virtual IGraphField GraphField { get; set; }

        public virtual IGraph Graph { get; protected set; }


        public MainModel(BaseGraphFieldFactory fieldFactory,
            IVertexEventHolder eventHolder,
            IGraphSerializer graphSerializer,
            IGraphAssembler graphFiller,
            IPathInput pathInput)
        {
            this.eventHolder = eventHolder;
            serializer = graphSerializer;
            this.fieldFactory = fieldFactory;
            this.graphFiller = graphFiller;
            this.pathInput = pathInput;

            Graph = new NullGraph();
            graphParamFormat = ViewModelResources.GraphParametresFormat;
        }

        public virtual void SaveGraph()
        {
            var savePath = pathInput.InputSavePath();
            try
            {
                using (var stream = new FileStream(savePath, FileMode.OpenOrCreate))
                {
                    serializer.SaveGraph(Graph, stream);
                }
            }
            catch { }
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
            catch { }
        }

        public abstract void FindPath();

        public abstract void CreateNewGraph();

        public virtual void ClearGraph()
        {
            Graph.Refresh();
            PathFindingStatistics = string.Empty;
            GraphParametres = Graph.GetFormattedData(graphParamFormat);
        }

        public void ConnectNewGraph(IGraph graph)
        {
            if (!graph.IsDefault)
            {
                Graph = graph;
                GraphField = fieldFactory.CreateGraphField(Graph);
                eventHolder.Graph = Graph;
                eventHolder.SubscribeVertices();
                GraphParametres = Graph.GetFormattedData(graphParamFormat);
                PathFindingStatistics = string.Empty;
            }
        }

        protected string graphParamFormat;
        protected readonly IGraphAssembler graphFiller;
        private readonly IVertexEventHolder eventHolder;
        private readonly IGraphSerializer serializer;
        private readonly BaseGraphFieldFactory fieldFactory;
        private readonly IPathInput pathInput;
    }
}
