using GraphLib.Interfaces;

namespace GraphViewModel.Interfaces
{
    public interface IMainModel
    {
        string GraphParametres { get; set; }

        IGraphField GraphField { get; set; }

        IGraph Graph { get; }

        void SaveGraph();

        void ConnectNewGraph(IGraph graph);

        void CreateNewGraph();

        void ClearGraph();

        void ClearColors();

        void LoadGraph();

        void FindPath();
    }
}