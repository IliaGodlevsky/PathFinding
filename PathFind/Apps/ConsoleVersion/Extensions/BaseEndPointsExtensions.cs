using ConsoleVersion.Model;
using GraphLib.Base.EndPoints;

namespace ConsoleVersion.Extensions
{
    internal static class BaseEndPointsExtensions
    {
        public static void RemoveSource(this BaseEndPoints self)
        {
            var source = (Vertex)self.Source;
            source.OnEndPointChosen();
        }

        public static void RemoveTarget(this BaseEndPoints self)
        {
            var target = (Vertex)self.Target;
            target.OnEndPointChosen();
        }
    }
}
