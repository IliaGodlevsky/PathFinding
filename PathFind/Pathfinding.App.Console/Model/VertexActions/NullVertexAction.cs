using Pathfinding.App.Console.Interface;
using Shared.Primitives.Single;

namespace Pathfinding.App.Console.Model.VertexActions
{
    internal sealed class NullVertexAction : Singleton<NullVertexAction, IVertexAction>, IVertexAction
    {
        private NullVertexAction()
        {

        }

        public void Do(Vertex vertex)
        {
            
        }
    }
}
