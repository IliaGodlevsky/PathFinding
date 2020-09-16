using GraphLibrary.DTO;
using GraphLibrary.EventHolder;
using GraphLibrary.GraphField;
using GraphLibrary.Graphs;
using GraphLibrary.Graphs.Interface;
using GraphLibrary.Vertex.Interface;
using System;

namespace GraphLibrary.ViewModel.Interface
{
    public interface IMainModel : IModel
    {
        AbstractVertexEventHolder VertexEventHolder { get; set; }        
        string GraphParametresFormat { get; }
        string GraphParametres { get; set; }
        IGraphField GraphField { get; set; }
        string Statistics { get; set; }
        void SetGraph(IGraph graph);
        void CreateNewGraph();
        IGraph Graph { get; }
        void SaveGraph();
        void ClearGraph();
        void LoadGraph();
        void FindPath();        
    }
}
