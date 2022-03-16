using GraphLib.Interfaces;
using Visualization.Abstractions;

namespace Visualization.Realizations
{
    internal sealed class ObstacleVertices : ResultVertices
    {
        protected override void Visualize(IVisualizable visualizable)
        {
            visualizable.VisualizeAsObstacle();
        }
    }
}
