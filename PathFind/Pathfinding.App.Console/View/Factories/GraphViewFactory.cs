using Pathfinding.App.Console.Interface;

namespace Pathfinding.App.Console.View.Factories
{
    internal sealed class GraphViewFactory : IViewFactory<GraphView>
    {
        private readonly IVertexBehavior behavior;

        public GraphViewFactory(IVertexBehavior behavior)
        {
            this.behavior = behavior;
        }

        public GraphView CreateView()
        {
            return new GraphView(new(), behavior);
        }
    }
}
