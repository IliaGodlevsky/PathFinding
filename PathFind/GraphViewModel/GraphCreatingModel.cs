using Common.Extensions.EnumerableExtensions;
using GraphLib.Interfaces;
using GraphLib.Interfaces.Factories;
using GraphViewModel.Interfaces;
using Logging.Interface;
using System.Collections.Generic;

namespace GraphLib.ViewModel
{
    public abstract class GraphCreatingModel : IModel
    {
        public int Width { get; set; }

        public int Length { get; set; }

        public int ObstaclePercent { get; set; }

        public IDictionary<string, IGraphAssemble> GraphAssembles { get; }

        public virtual IGraphAssemble SelectedGraphAssemble { get; set; }

        protected GraphCreatingModel(ILog log,
            IEnumerable<IGraphAssemble> graphAssembles)
        {
            GraphAssembles = graphAssembles.ToNameInstanceDictionary();
            this.graphAssembles = graphAssembles;
            this.log = log;
        }

        public abstract void CreateGraph();

        protected virtual int[] GraphParametres => new[] { Width, Length };

        protected IGraph graph;
        protected readonly IEnumerable<IGraphAssemble> graphAssembles;
        protected readonly ILog log;
    }
}
