namespace GraphLib.Interfaces.Factories
{
    public interface IVertexFactory<out TVertex>
        where TVertex : IVertex
    {
        TVertex CreateVertex(ICoordinate coordinate);
    }
}
