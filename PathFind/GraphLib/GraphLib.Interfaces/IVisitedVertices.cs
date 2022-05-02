namespace GraphLib.Interfaces
{
    public interface IVisitedVertices
    {
        void Visit(IVertex vertex);

        bool IsNotVisited(IVertex vertex);

        void Clear();
    }
}
