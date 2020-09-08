using GraphLibrary.Vertex.Interface;

namespace GraphLibrary.GraphField
{
    public interface IGraphField
    {
        void Add(IVertex vertex, int xCoordinate, int yCoordinate);
    }
}
