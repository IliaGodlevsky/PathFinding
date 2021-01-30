using GraphLib.EventHolder.Interface;
using GraphLib.Extensions;
using GraphLib.GraphField;
using GraphLib.GraphFieldCreating;
using GraphLib.Graphs;
using GraphLib.Graphs.Abstractions;
using GraphLib.Graphs.Serialization;
using GraphLib.Graphs.Serialization.Interfaces;
using GraphLib.Info;
using GraphLib.Vertex.Interface;
using GraphViewModel.Interfaces;
using GraphViewModel.Resources;
using System;
using System.IO;

namespace GraphViewModel
{
    public abstract class MainModel : IMainModel
    {
        public virtual string GraphParametres { get; set; }

        public virtual string PathFindingStatistics { get; set; }

        public virtual IGraphField GraphField { get; set; }

        public virtual IGraph Graph { get; protected set; }

        public IVertexEventHolder VertexEventHolder { get; set; }

        public IGraphSerializer Serializer { get; set; }

        public BaseGraphFieldFactory FieldFactory { get; set; }

        public Func<VertexSerializationInfo, IVertex> SerializationInfoConverter { get; set; }

        public MainModel()
        {
            Graph = new NullGraph();
            Serializer = new GraphSerializer<Graph2D>();
            graphParamFormat = ViewModelResources.GraphParametresFormat;
        }

        public virtual void SaveGraph()
        {
            var savePath = GetSavingPath();
            try
            {
                using (var stream = new FileStream(savePath, FileMode.OpenOrCreate))
                {
                    Serializer.SaveGraph(Graph, stream);
                }
            }
            catch { }
        }

        public virtual void LoadGraph()
        {
            var loadPath = GetLoadingPath();
            try
            {
                using (var stream = new FileStream(loadPath, FileMode.Open))
                {
                    var newGraph = Serializer.LoadGraph(stream, SerializationInfoConverter);
                    ConnectNewGraph(newGraph);
                }
            }
            catch { }
        }

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
                GraphField = FieldFactory.CreateGraphField(Graph);
                VertexEventHolder.Graph = Graph;
                VertexEventHolder.SubscribeVertices();
                GraphParametres = Graph.GetFormattedData(graphParamFormat);
                PathFindingStatistics = string.Empty;
            }
        }

        protected string graphParamFormat;

        public abstract void FindPath();

        public abstract void CreateNewGraph();

        protected abstract string GetSavingPath();

        protected abstract string GetLoadingPath();
    }
}
