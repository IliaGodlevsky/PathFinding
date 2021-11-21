using GraphLib.Interfaces;
using GraphLib.Serialization.Extensions;
using GraphLib.Serialization.Interfaces;

namespace GraphLib.Serialization
{
    public class GraphSerializationModule
    {
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

        internal readonly IGraphSerializer serializer;
        internal readonly IPathInput input;
    }
}
