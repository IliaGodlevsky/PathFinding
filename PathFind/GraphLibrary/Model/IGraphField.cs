using GraphLibrary.Vertex;

namespace GraphLibrary.Model
{
    public interface IGraphField
    {
        void Add(IVertex vertex, int xCoordinate, int yCoordinate);
    }
}
