using GraphLib.Interfaces;
using GraphLib.Serialization.Exceptions;
using GraphLib.Serialization.Interfaces;
using GraphViewModel.Interfaces;
using Logging.Interface;
using Logging.Loggers;
using System;
using System.IO;
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
            using (var stream = new FileStream(loadPath, FileMode.Open))
            {
                var newGraph = graphSerializer.LoadGraph(stream);
                return newGraph;
            }
        }

        public void SaveGraph(IGraph graph)
        {
            string savePath = pathInput.InputSavePath();
            using (var stream = new FileStream(savePath, FileMode.OpenOrCreate))
            {
                graphSerializer.SaveGraph(graph, stream);
            }
        }

        public async Task SaveGraphAsync(IGraph graph)
        {
            string savePath = pathInput.InputSavePath();
            await Task.Run(() =>
            {
                try
                {
                    using (var stream = new FileStream(savePath, FileMode.OpenOrCreate))
                    {
                        graphSerializer.SaveGraph(graph, stream);
                    }
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
