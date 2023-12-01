namespace Pathfinding.App.Console.Model.Visualizations.Containers
{
    internal sealed class VisualizedVisited : VisualizedVertices
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
