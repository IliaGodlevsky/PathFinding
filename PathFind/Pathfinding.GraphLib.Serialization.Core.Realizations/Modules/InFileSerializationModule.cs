using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Serialization.Core.Interface;
using Pathfinding.GraphLib.Serialization.Core.Realizations.Extensions;

namespace Pathfinding.GraphLib.Serialization.Core.Realizations.Modules
{
    public sealed class InFileSerializationModule<TVertex> : IGraphSerializationModule<TVertex>
        where TVertex : IVertex
    {
        private readonly ISerializer<IGraph<TVertex>> serializer;
        private readonly IPathInput input;

        public InFileSerializationModule(ISerializer<IGraph<TVertex>> graphSerializer, IPathInput pathInput)
        {
            serializer = graphSerializer;
            input = pathInput;
        }

        public IGraph<TVertex> LoadGraph()
        {
            string loadPath = input.InputLoadPath();
            return serializer.DeserializeFromFile(loadPath);
        }

        public void SaveGraph(IGraph<TVertex> graph)
        {
            string savePath = input.InputSavePath();
            serializer.SerializeToFile(graph, savePath);
        }
    }
}
