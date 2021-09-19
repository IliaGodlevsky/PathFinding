using Common.Interface;
using System.Threading.Tasks;

namespace Common.Extensions
{
    public static class CloneableExtemsions
    {
        public static async Task<T> CloneAsync<T>(this ICloneable<T> self)
        {
            return await Task.Run(self.Clone).ConfigureAwait(false);
        }
    }
}
