using Common.Extensions.EnumerableExtensions;
using GraphLib.Interfaces;
using GraphLib.Interfaces.Factories;
using Logging.Interface;
using System.Collections.Generic;

namespace GraphLib.ViewModel
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
