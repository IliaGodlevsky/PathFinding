using GraphLib.Interfaces;
using System.Threading.Tasks;

namespace GraphViewModel.Interfaces
{
    public interface ISaveLoadGraph
    {
        Task SaveGraphAsync(IGraph graph);

        Task<IGraph> LoadGraphAsync();
    }
}
