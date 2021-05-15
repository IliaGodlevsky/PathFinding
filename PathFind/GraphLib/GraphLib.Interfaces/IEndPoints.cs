namespace GraphLib.Interfaces
{
    public interface IEndPoints
    {
        IVertex Target { get; }

        IVertex Source { get; }

        bool IsEndPoint(IVertex vertex);
    }
}
