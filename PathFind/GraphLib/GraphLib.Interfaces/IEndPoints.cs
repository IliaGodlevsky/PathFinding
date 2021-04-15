namespace GraphLib.Interfaces
{
    public interface IEndPoints
    {
        IVertex End { get; }

        IVertex Start { get; }

        bool IsEndPoint(IVertex vertex);
    }
}
