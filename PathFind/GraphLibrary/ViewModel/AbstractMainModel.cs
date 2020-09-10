using GraphLibrary.DTO;
using GraphLibrary.EventHolder;
using GraphLibrary.Extensions.CustomTypeExtensions;
using GraphLibrary.GraphCreate.GraphFieldFactory;
using GraphLibrary.GraphField;
using GraphLibrary.Graphs;
using GraphLibrary.GraphSerialization.GraphLoader;
using GraphLibrary.GraphSerialization.GraphLoader.Interface;
using GraphLibrary.GraphSerialization.GraphSaver;
using GraphLibrary.GraphSerialization.GraphSaver.Interface;
using GraphLibrary.Vertex.Interface;
using GraphLibrary.ViewModel.Interface;
using System;

namespace GraphLibrary.ViewModel
{
    public abstract class AbstractMainModel : IMainModel
    {
        public virtual string GraphParametres { get; set; }
        public virtual string Statistics { get; set; }
        public virtual IGraphField GraphField { get; set; }
        public virtual Graph Graph { get; protected set; }
        public AbstractVertexEventHolder VertexEventHolder { get; set; }
        public string GraphParametresFormat { get; protected set; }

        public AbstractMainModel()
        {
            saver = new GraphSaver();
            loader = new GraphLoader();
        }

        public virtual void SaveGraph()
        {
            saver.SaveGraph(Graph, GetSavePath());
        }

        public virtual void LoadGraph()
        {
            var temp = loader.GetGraph(GetLoadPath(), generator);
            if (temp == null)
                return;
            SetGraph(temp);
        }

        public virtual void ClearGraph()
        {
            Graph.Refresh();
            Statistics = string.Empty;
            GraphParametres = Graph.GetFormattedInfo(GraphParametresFormat);
        }
        
        public abstract void FindPath();
        public abstract void CreateNewGraph();

        public void SetGraph(Graph graph)
        {
            Graph = graph;
            GraphField = graphFieldFactory.GetGraphField(Graph);
            VertexEventHolder.Graph = Graph;
            VertexEventHolder.ChargeGraph();
            GraphParametres = Graph.GetFormattedInfo(GraphParametresFormat);
            Statistics = string.Empty;
        }

        protected abstract string GetSavePath();
        protected abstract string GetLoadPath();

        protected Func<VertexDto, IVertex> generator;
        protected IGraphSaver saver;
        protected IGraphLoader loader;
        protected AbstractGraphFieldFactory graphFieldFactory;
    }
}
