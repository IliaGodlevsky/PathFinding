using Algorithm.Interfaces;
using NullObject.Extensions;
using WPFVersion.Enums;

namespace WPFVersion.Extensions
{
    internal static class IGraphPathExtensions
    {
        public static AlgorithmStatus ToStatus(this IGraphPath self)
        {
            return self.IsNull() ? AlgorithmStatus.Failed : AlgorithmStatus.Finished;
        }
    }
}
