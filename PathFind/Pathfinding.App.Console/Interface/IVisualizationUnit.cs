using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Interface;

namespace Pathfinding.App.Console.Interface
{
    internal interface IVisualizationUnit
    {
        void Visualize(IGraph<Vertex> graph);
    }
}
