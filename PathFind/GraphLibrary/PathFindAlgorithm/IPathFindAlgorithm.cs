namespace GraphLibrary.Algorithm
{
    public delegate void PauseCycle(int milliseconds);

    /// <summary>
    /// A base interface of path find algorithms
    /// </summary>
    public interface IPathFindAlgorithm
    {
        PauseCycle Pause { set; get; }
        bool FindDestionation();
        void DrawPath();
        string GetStatistics();
    }
}
