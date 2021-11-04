using Algorithm.Infrastructure.EventArguments;
using Algorithm.Interfaces;
using Algorithm.NullRealizations;
using GraphLib.Interfaces;
using GraphLib.NullRealizations.NullObjects;

namespace GraphViewModel.Extensions
{
    public static class VisualizationModelExtensions
    {
        public static bool CanVisualize(this VisualizationModel self, object sender,
            AlgorithmEventArgs e, out IAlgorithm algorithm, out IVisualizable vertex)
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
