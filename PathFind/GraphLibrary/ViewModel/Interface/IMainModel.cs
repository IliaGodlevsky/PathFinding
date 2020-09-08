using GraphLibrary.EventHolder;
using GraphLibrary.GraphField;
using GraphLibrary.Graphs;

namespace GraphLibrary.ViewModel.Interface
{
    public interface IMainModel : IModel
    {
        string GraphParametres { get; set; }
        string Statistics { get; set; }
        IGraphField GraphField { get; set; }
        Graph Graph { get; set; }
        AbstractVertexEventHolder VertexEventHolder { get; set; }
        string GraphParametresFormat { get; }
        void SaveGraph();
        void LoadGraph();
        void ClearGraph();
        void FindPath();
        void CreateNewGraph();
    }
}
