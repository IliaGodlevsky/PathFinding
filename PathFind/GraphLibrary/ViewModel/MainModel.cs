using GraphLibrary.DTO;
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
using GraphLibrary.Vertex.Interface;
using GraphLibrary.ViewModel.Interface;
using System;

namespace GraphLibrary.ViewModel
{
    public abstract class MainModel : IMainModel
    {
        public virtual string GraphParametres { get; set; }
        public virtual string Statistics { get; set; }
        public virtual IGraphField GraphField { get; set; }
        public virtual IGraph Graph { get; protected set; }
        public IVertexEventHolder VertexEventHolder { get; set; }
        public string GraphParametresFormat { get; protected set; }
        public IGraphSaver Saver { get; set; }
        public IGraphLoader Loader { get; set; }
        public GraphFieldFactory FieldFactory { get; set; }
        public Func<VertexDto, IVertex> DtoConverter { get; set; }


        public MainModel()
        {
            Graph = NullGraph.Instance;
            Saver = new GraphSaver();
            Loader = new GraphLoader();
        }

        public virtual void SaveGraph()
        {
            Saver.SaveGraph(Graph, GetSavingPath());
        }

        public virtual void LoadGraph()
        {
            var temp = Loader.LoadGraph(GetLoadingPath(), DtoConverter);
            if (temp != NullGraph.Instance)
                SetGraph(temp);
        }

        public virtual void ClearGraph()
        {
            Graph.Refresh();
            Statistics = string.Empty;
            GraphParametres = Graph.GetFormattedInfo(GraphParametresFormat);
        }

        public void SetGraph(IGraph graph)
        {
            Graph = graph;
            GraphField = FieldFactory.CreateGraphField(Graph);
            VertexEventHolder.Graph = Graph;
            VertexEventHolder.ChargeGraph();
            GraphParametres = Graph.GetFormattedInfo(GraphParametresFormat);
            Statistics = string.Empty;
        }

        public abstract void FindPath();
        public abstract void CreateNewGraph();

        protected abstract string GetSavingPath();
        protected abstract string GetLoadingPath();
    }
}
