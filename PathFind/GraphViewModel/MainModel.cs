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

        public Func<VertexInfo, IVertex> InfoConverter { get; set; }

        public MainModel()
        {
            Graph = new NullGraph();
            Serializer = new GraphSerializer<Graph2D>();
            graphParamFormat = ViewModelResources.GraphParametresFormat;
        }

        public virtual void SaveGraph()
        {
            var savePath = GetSavingPath();
            Serializer.SaveGraph(Graph, savePath);
        }

        public virtual void LoadGraph()
        {
            var loadPath = GetLoadingPath();
            var newGraph = Serializer.LoadGraph(loadPath, InfoConverter);
            ConnectNewGraph(newGraph);
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
