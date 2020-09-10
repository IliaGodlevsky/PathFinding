using GraphLibrary.DTO;
using GraphLibrary.EventHolder;
using GraphLibrary.GraphField;
using GraphLibrary.Graphs;
using GraphLibrary.Vertex.Interface;
using System;

namespace GraphLibrary.ViewModel.Interface
{
    public interface IMainModel : IModel
    {
        AbstractVertexEventHolder VertexEventHolder { get; set; }
        void LoadGraph(Func<VertexDto, IVertex> generator);
        string GraphParametresFormat { get; }
        string GraphParametres { get; set; }
        IGraphField GraphField { get; set; }
        string Statistics { get; set; }
        void SetGraph(Graph graph);
        void CreateNewGraph();
        Graph Graph { get; }
        void SaveGraph();
        void ClearGraph();
        void FindPath();
    }
}
