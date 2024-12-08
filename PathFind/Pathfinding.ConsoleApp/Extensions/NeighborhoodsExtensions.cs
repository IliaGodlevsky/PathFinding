using Pathfinding.Domain.Core;

namespace Pathfinding.ConsoleApp.Extensions
{
    internal static class NeighborhoodsExtensions
    {
        public static string ToStringRepresentation(this Neighborhoods neighborhood)
        {
            return neighborhood switch
            {
                Neighborhoods.Moore => "Moore",
                Neighborhoods.VonNeumann => "Neumann",
                _ => "",
            };
        }
    }
}
