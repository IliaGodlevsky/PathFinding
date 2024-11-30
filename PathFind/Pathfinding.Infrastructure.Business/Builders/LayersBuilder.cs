using Pathfinding.Domain.Core;
using Pathfinding.Domain.Interface;
using Pathfinding.Infrastructure.Business.Layers;
using Pathfinding.Service.Interface.Models;

namespace Pathfinding.Infrastructure.Business.Builders
{
    public sealed class LayersBuilder
    {
        private readonly Neighborhoods neighborhoods;
        private readonly SmoothLevels smoothLevels;

        public static LayersBuilder Take(IGraphLayersInfo info) 
            => new (info.Neighborhood, info.SmoothLevel);

        public ILayer Build()
        {
            var layers = new Layers.Layers();
            switch (neighborhoods)
            {
                case Neighborhoods.Moore:
                    layers.Add(new MooreNeighborhoodLayer());
                    break;
                case Neighborhoods.VonNeumann:
                    layers.Add(new VonNeumannNeighborhoodLayer());
                    break;
            }
            int level = default;
            switch (smoothLevels)
            {
                case SmoothLevels.Low: level = 1; break;
                case SmoothLevels.Medium: level = 2; break;
                case SmoothLevels.High: level = 4; break;
                case SmoothLevels.Extreme: level = 7; break;
            }
            layers.Add(new SmoothLayer(level));
            return layers;
        }

        private LayersBuilder(Neighborhoods neighborhoods, 
            SmoothLevels smoothLevels)
        {
            this.neighborhoods = neighborhoods;
            this.smoothLevels = smoothLevels;
        }
    }
}
