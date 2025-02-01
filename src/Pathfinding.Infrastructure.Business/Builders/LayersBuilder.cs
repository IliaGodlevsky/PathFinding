using Pathfinding.Domain.Core;
using Pathfinding.Domain.Interface;
using Pathfinding.Infrastructure.Business.Layers;
using Pathfinding.Service.Interface.Models;

namespace Pathfinding.Infrastructure.Business.Builders
{
    public sealed class LayersBuilder
    {
        public static ILayer Build(IGraphLayersInfo info)
        {
            var neighborhood = info.Neighborhood switch
            {
                Neighborhoods.Moore => new MooreNeighborhoodLayer(),
                Neighborhoods.VonNeumann => new VonNeumannNeighborhoodLayer(),
                _ => (ILayer)new Layers.Layers()
            };
            SmoothLayer smooth = info.SmoothLevel switch
            {
                SmoothLevels.Low => new(1),
                SmoothLevels.Medium => new(2),
                SmoothLevels.High => new(4),
                SmoothLevels.Extreme => new(7),
                _ => new(0)
            };
            return new Layers.Layers() { neighborhood, smooth };
        }
    }
}
