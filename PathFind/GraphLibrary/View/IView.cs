namespace GraphLibrary.View
{
    public interface IView
    {
        void SaveGraph();
        void LoadGraph();
        void FindPath();
        void CreateGraph();
        void RefreshGraph();
    }
}
