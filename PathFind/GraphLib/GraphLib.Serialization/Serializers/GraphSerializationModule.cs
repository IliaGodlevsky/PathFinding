using GraphLib.Interfaces;
using GraphLib.Serialization.Extensions;
using GraphLib.Serialization.Interfaces;

namespace GraphLib.Serialization
{
    public sealed class GraphSerializationModule : IGraphSerializationModule
    {
        private readonly IGraphSerializer serializer;
        private readonly IPathInput input;

        public GraphSerializationModule(IGraphSerializer graphSerializer, IPathInput pathInput)
        {
            serializer = graphSerializer;
            input = pathInput;
        }

        public IGraph LoadGraph()
        {
            string loadPath = input.InputLoadPath();
            return serializer.LoadGraphFromFile(loadPath);
        }

        public void SaveGraph(IGraph graph)
        {
            string savePath = input.InputSavePath();
            serializer.SaveGraphToFile(graph, savePath);
        }
    }
}
