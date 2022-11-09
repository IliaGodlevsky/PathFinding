using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Factory.Interface;
using Pathfinding.Logging.Interface;
using Pathfinding.VisualizationLib.Core.Interface;
using Shared.Extensions;
using System.Collections.Generic;

namespace Pathfinding.Visualization.Models
{
    public abstract class GraphCreatingModel<TGraph, TVertex>
        where TGraph : IGraph<TVertex>
        where TVertex : IVertex, IVisualizable
    {
        protected readonly ILog log;

        public int Width { get; set; }

        public int Length { get; set; }

        public int ObstaclePercent { get; set; }

        public IReadOnlyList<IGraphAssemble<TGraph, TVertex>> GraphAssembles { get; }

        public virtual IGraphAssemble<TGraph, TVertex> SelectedGraphAssemble { get; set; }

        protected GraphCreatingModel(ILog log, IEnumerable<IGraphAssemble<TGraph, TVertex>> graphAssembles)
        {
            this.log = log;
            GraphAssembles = graphAssembles.ToReadOnly();
        }

        public abstract void CreateGraph();
    }
}
