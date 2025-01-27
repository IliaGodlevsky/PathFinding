using Pathfinding.Domain.Core;

namespace Pathfinding.Service.Interface.Models
{
    public interface IGraphLayersInfo
    {
        Neighborhoods Neighborhood { get; }

        SmoothLevels SmoothLevel { get; }
    }
}
