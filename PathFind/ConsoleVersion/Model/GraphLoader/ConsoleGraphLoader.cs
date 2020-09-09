using ConsoleVersion.Model.GraphFactory;
using GraphLibrary.DTO;
using GraphLibrary.GraphFactory;
using GraphLibrary.GraphSerialization.GraphLoader;

namespace ConsoleVersion.Model.GraphLoader
{
    internal class ConsoleGraphLoader : AbstractGraphLoader
    {
        protected override AbstractGraphInfoInitializer GetInitializer(VertexDto[,] info) => new ConsoleGraphInitializer(info);
    }
}
