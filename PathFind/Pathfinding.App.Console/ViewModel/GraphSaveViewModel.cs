using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Model;
using Pathfinding.App.Console.Model.Menu.Attributes;
using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using Pathfinding.GraphLib.Serialization.Core.Interface;
using Pathfinding.Logging.Interface;

namespace Pathfinding.App.Console.ViewModel
{
    [MenuColumnsNumber(1)]
    internal sealed class GraphSaveViewModel : SafeViewModel
    {
        private readonly IGraphSerializationModule<Graph2D<Vertex>, Vertex> module;

        private IGraph<Vertex> Graph { get; set; } = Graph2D<Vertex>.Empty;

        public GraphSaveViewModel(IGraphSerializationModule<Graph2D<Vertex>, Vertex> module, 
            ICache<Graph2D<Vertex>> graph, ILog log)
            : base(log)
        {
            this.module = module;
            Graph = graph.Cached;
        }

        [ExecuteSafe(nameof(ExecuteSafe))]
        [MenuItem(MenuItemsNames.SaveGraph, 0)]
        private void SaveGraph()
        {
            using (Cursor.ClearInputToCurrentPosition())
            {
                module.SaveGraph(Graph);
            }
        }

        private bool IsGraphValid()
        {
            return Graph != Graph2D<Vertex>.Empty;
        }
    }
}