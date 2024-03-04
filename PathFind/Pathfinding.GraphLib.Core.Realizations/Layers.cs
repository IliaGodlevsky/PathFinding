using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Factory.Interface;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.GraphLib.Core.Realizations
{
    public class Layers : ILayer
    {
        private readonly IEnumerable<ILayer> layers;

        public Layers(params ILayer[] layers)
        {
            this.layers = layers;
        }

        public Layers(IEnumerable<ILayer> layers)
            : this(layers.ToArray())
        {

        }

        public virtual void Overlay(IGraph<IVertex> graph)
        {
            foreach (var layer in layers)
            {
                layer.Overlay(graph);
            }
        }
    }
}
