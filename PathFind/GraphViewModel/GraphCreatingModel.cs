using Common.Extensions.EnumerableExtensions;
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

        protected GraphCreatingModel(IEnumerable<IGraphAssemble> graphAssembles)
        {
            GraphAssembles = graphAssembles.ToNameInstanceDictionary();
        }

        public abstract void CreateGraph();

        protected ILog log;
    }
}
