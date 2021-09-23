using GraphLib.Interfaces;
using GraphLib.Serialization.Exceptions;
using GraphLib.Serialization.Extensions;
using GraphLib.Serialization.Interfaces;
using GraphViewModel.Interfaces;
using System;
using System.Threading.Tasks;

namespace GraphViewModel
{
    public sealed class SaveLoadGraph : ISaveLoadGraph
    {
        public SaveLoadGraph(IGraphSerializer graphSerializer, IPathInput pathInput)
        {
            this.graphSerializer = graphSerializer;
            this.pathInput = pathInput;
        }

        public async Task<IGraph> LoadGraphAsync()
        {
            string loadPath = pathInput.InputLoadPath();
            return await Task.Run(() =>
            {
                try
                {
                    return graphSerializer.LoadGraphFromFile(loadPath);
                }
                catch (Exception)
                {
                    throw;
                }
            }).ConfigureAwait(false);
        }

        public async Task SaveGraphAsync(IGraph graph)
        {
            string savePath = pathInput.InputSavePath();
            await Task.Run(() =>
            {
                try
                {
                    graphSerializer.SaveGraphToFile(graph, savePath);
                }
                catch (Exception)
                {
                    throw;
                }
            }).ConfigureAwait(false);
        }

        private readonly IGraphSerializer graphSerializer;
        private readonly IPathInput pathInput;
    }
}
