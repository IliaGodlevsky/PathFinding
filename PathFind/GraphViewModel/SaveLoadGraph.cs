using GraphLib.Interfaces;
using GraphLib.Serialization.Exceptions;
using GraphLib.Serialization.Extensions;
using GraphLib.Serialization.Interfaces;
using GraphViewModel.Interfaces;
using Logging.Interface;
using Logging.Loggers;
using System;
using System.Threading.Tasks;

namespace GraphViewModel
{
    public sealed class SaveLoadGraph : ISaveLoadGraph
    {
        public SaveLoadGraph(Logs log, IGraphSerializer graphSerializer,
            IPathInput pathInput)
        {
            this.graphSerializer = graphSerializer;
            this.pathInput = pathInput;
            this.log = log;
        }

        public IGraph LoadGraph()
        {
            string loadPath = pathInput.InputLoadPath();
            return graphSerializer.LoadFromFile(loadPath);
        }

        public void SaveGraph(IGraph graph)
        {
            string savePath = pathInput.InputSavePath();
            graphSerializer.SaveToFile(graph, savePath);
        }

        public async Task SaveGraphAsync(IGraph graph)
        {
            string savePath = pathInput.InputSavePath();
            await Task.Run(() =>
            {
                try
                {
                    graphSerializer.SaveToFile(graph, savePath);
                }
                catch (CantSerializeGraphException ex)
                {
                    log.Warn(ex);
                }
                catch (Exception ex)
                {
                    log.Error(ex);
                }
            });
        }

        private readonly IGraphSerializer graphSerializer;
        private readonly IPathInput pathInput;
        private readonly ILog log;
    }
}
