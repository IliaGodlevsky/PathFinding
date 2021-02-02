using GraphLib.Interface;

namespace GraphViewModel.Interfaces
{
    public interface IMainModel : IModel
    {
        string GraphParametres { get; set; }

        IGraphField GraphField { get; set; }

        string PathFindingStatistics { get; set; }

        IGraph Graph { get; }

        void SaveGraph();

        void ConnectNewGraph(IGraph graph);

        void CreateNewGraph();

        void ClearGraph();

        void LoadGraph();

        void FindPath();
    }
}
