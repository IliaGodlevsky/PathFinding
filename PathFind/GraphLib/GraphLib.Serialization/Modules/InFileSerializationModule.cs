using GraphLib.Interfaces;
using GraphLib.Serialization.Extensions;
using GraphLib.Serialization.Interfaces;

namespace GraphLib.Serialization.Modules
{
    public sealed class InFileSerializationModule<TGraph, TVertex> : IGraphSerializationModule<TGraph, TVertex>
        where TGraph : IGraph<TVertex>
        where TVertex : IVertex
    {
        private readonly IGraphSerializer<TGraph, TVertex> serializer;
        private readonly IPathInput input;

        public InFileSerializationModule(IGraphSerializer<TGraph, TVertex> graphSerializer, IPathInput pathInput)
        {
            serializer = graphSerializer;
            input = pathInput;
        }

        public TGraph LoadGraph()
        {
            string loadPath = input.InputLoadPath();
            return serializer.LoadGraphFromFile(loadPath);
        }

        public void SaveGraph(IGraph<IVertex> graph)
        {
            string savePath = input.InputSavePath();
            serializer.SaveGraphToFile(graph, savePath);
        }
    }
}
