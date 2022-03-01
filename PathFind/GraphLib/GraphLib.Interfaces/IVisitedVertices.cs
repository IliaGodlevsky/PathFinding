namespace GraphLib.Interfaces
{
    public interface IVisitedVertices
    {
        void Add(IVertex vertex);

        bool IsNotVisited(IVertex vertex);

        void Clear();
    }
}
