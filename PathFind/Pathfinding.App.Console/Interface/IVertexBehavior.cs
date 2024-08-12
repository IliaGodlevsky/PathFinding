using Pathfinding.App.Console.View;

namespace Pathfinding.App.Console.Interface
{
    public interface IVertexBehavior
    {
        void AddBehavior(VertexView view);

        void RemoveBehavior(VertexView view);
    }
}
