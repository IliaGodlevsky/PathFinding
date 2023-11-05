using Pathfinding.App.Console.Settings;

namespace Pathfinding.App.Console.Model.Visualizations.Containers
{
    internal sealed class VisualizedEnqueued : VisualizedVertices
    {
        public override bool Add(int id, Vertex vertex)
        {
            if (!vertex.IsVisualizedAsRange() && !vertex.IsVisualizedAsPath())
            {
                return base.Add(id, vertex);
            }
            return false;
        }
    }
}
