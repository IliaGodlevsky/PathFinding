using GraphLibrary.EventHolder.Interface;
using GraphLibrary.Extensions.CustomTypeExtensions;
using GraphLibrary.GraphField;
using GraphLibrary.GraphFieldCreating;
using GraphLibrary.Graphs;
using GraphLibrary.Graphs.Interface;
using GraphLibrary.GraphSerialization.GraphLoader;
using GraphLibrary.GraphSerialization.GraphLoader.Interface;
using GraphLibrary.GraphSerialization.GraphSaver;
using GraphLibrary.GraphSerialization.GraphSaver.Interface;
using GraphLibrary.Info;
using GraphLibrary.Vertex.Interface;
using GraphLibrary.ViewModel.Interface;
using System;

namespace GraphLibrary.ViewModel
{
    public abstract class MainModel : IMainModel
    {
        public virtual string GraphParametres { get; set; }
        public virtual string PathFindingStatistics { get; set; }
        public virtual IGraphField GraphField { get; set; }
        public virtual IGraph Graph { get; protected set; }
        public IVertexEventHolder VertexEventHolder { get; set; }
        public IGraphSaver Saver { get; set; }
        public IGraphLoader Loader { get; set; }
        public GraphFieldFactory FieldFactory { get; set; }
        public Func<VertexInfo, IVertex> DtoConverter { get; set; }

        public MainModel()
        {
            Graph = NullGraph.Instance;
            Saver = new GraphSaver();
            Loader = new GraphLoader();
        }

        public virtual void SaveGraph()
        {
            Saver.SaveInFile(Graph, GetSavingPath());
        }

        public virtual void LoadGraph()
        {
            var newGraph = Loader.LoadGraph(GetLoadingPath(), DtoConverter);
            if (!ReferenceEquals(newGraph, NullGraph.Instance))
                ConnectNewGraph(newGraph);
        }

        public virtual void ClearGraph()
        {
            Graph.Refresh();
            PathFindingStatistics = string.Empty;
            GraphParametres = Graph.GetFormattedData(LibraryResources.GraphParametresFormat);
        }

        public void ConnectNewGraph(IGraph graph)
        {
            Graph = graph;
            GraphField = FieldFactory.CreateGraphField(Graph);
            VertexEventHolder.Graph = Graph;
            VertexEventHolder.SubscribeVertices();
            GraphParametres = Graph.GetFormattedData(LibraryResources.GraphParametresFormat);
            PathFindingStatistics = string.Empty;
        }

        public abstract void FindPath();
        public abstract void CreateNewGraph();
        protected abstract string GetSavingPath();
        protected abstract string GetLoadingPath();
    }
}
