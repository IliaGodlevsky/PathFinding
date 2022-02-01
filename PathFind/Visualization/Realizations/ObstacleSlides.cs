using GraphLib.Interfaces;
using Visualization.Abstractions;
using Visualization.Interfaces;

namespace Visualization.Realizations
{
    internal sealed class ObstacleSlides : GraphSlides<bool>, IVisualization
    {
        protected override bool GetActual(IVertex vertex)
        {
            return GetStored(vertex);
        }

        protected override bool GetStored(IVertex vertex)
        {
            return vertex.IsObstacle;
        }

        protected override void SetStored(IVertex vertex, bool obstacle)
        {
            vertex.IsObstacle = obstacle;
        }
    }
}
