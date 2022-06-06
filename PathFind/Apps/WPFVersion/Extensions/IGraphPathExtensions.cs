using Algorithm.Interfaces;
using NullObject.Extensions;
using WPFVersion.ViewModel;

namespace WPFVersion.Extensions
{
    internal static class IGraphPathExtensions
    {
        public static string ToStatus(this IGraphPath self)
        {
            return self.IsNull() ? AlgorithmViewModel.Failed : AlgorithmViewModel.Finished;
        }
    }
}
