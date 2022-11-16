using Pathfinding.AlgorithmLib.Core.Interface;
using Pathfinding.App.WPF._2D.ViewModel;

namespace Pathfinding.App.WPF._2D.Extensions
{
    internal static class IGraphPathExtensions
    {
        public static string ToStatus(this IGraphPath self)
        {
            return self.Count == 0 ? AlgorithmViewModel.Failed : AlgorithmViewModel.Finished;
        }
    }
}
