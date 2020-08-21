using GraphLibrary.Graph;

namespace GraphLibrary.Model
{
    public interface IMainModel : IModel
    {
        string GraphParametres { get; set; }
        string Statistics { get; set; }
        IGraphField GraphField { get; set; }
        AbstractGraph Graph { get; set; }
        string Format { get; }
        void SaveGraph();
        void LoadGraph();
        void ClearGraph();
        void PathFind();
        void CreateNewGraph();
    }
}
