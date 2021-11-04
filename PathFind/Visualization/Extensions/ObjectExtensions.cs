using Algorithm.Infrastructure.EventArguments;
using Algorithm.Interfaces;
using Algorithm.NullRealizations;
using GraphLib.Interfaces;
using GraphLib.NullRealizations.NullObjects;

namespace Visualization.Extensions
{
    internal static class ObjectExtensions
    {
        public static bool CanBeVisualized(this object sender, AlgorithmEventArgs e, out IAlgorithm algorithm, out IVisualizable vertex)
        {
            algorithm = NullAlgorithm.Instance;
            vertex = NullVisualizable.Instance;
            if (e.Current is IVisualizable vert
                && sender is IAlgorithm algo
                && !vert.IsVisualizedAsEndPoint)
            {
                algorithm = algo;
                vertex = vert;
                return true;
            }

            return false;
        }
    }
}
