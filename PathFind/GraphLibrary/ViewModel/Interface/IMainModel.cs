using GraphLibrary.EventHolder.Interface;
using GraphLibrary.GraphField;
using GraphLibrary.Graphs.Interface;

namespace GraphLibrary.ViewModel.Interface
{
    public interface IMainModel : IModel
    {
        IVertexEventHolder VertexEventHolder { get; set; }        
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
