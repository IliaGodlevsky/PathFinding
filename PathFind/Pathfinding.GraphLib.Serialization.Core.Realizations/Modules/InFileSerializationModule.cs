using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Serialization.Core.Interface;
using Pathfinding.GraphLib.Serialization.Core.Realizations.Extensions;

namespace Pathfinding.GraphLib.Serialization.Core.Realizations.Modules
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
            return serializer.DeserializeFromFile(loadPath);
        }

        public void SaveGraph(TGraph graph)
        {
            string savePath = input.InputSavePath();
            serializer.SerializeToFile(graph, savePath);
        }
    }
}
