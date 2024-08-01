using System.Threading;
using System.Threading.Tasks;

namespace Pathfinding.App.Console.Interface
{
    internal interface IMenuItem
    {
        Task ExecuteAsync(CancellationToken token = default);
    }
}
