using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using Pathfinding.GraphLib.Serialization.Core.Interface;
using Pathfinding.GraphLib.Serialization.Core.Realizations.Extensions;
using Pathfinding.Logging.Interface;
using System.Threading.Tasks;

namespace Pathfinding.App.Console.MenuItems.GraphMenuItems
{
    [MediumPriority]
    internal sealed class SaveGraphMenuItem : ExportGraphMenuItem<string>
    {
        public SaveGraphMenuItem(IMessenger messenger, IFilePathInput input,
            IGraphSerializer<Graph2D<Vertex>, Vertex> serializer, ILog log)
            : base(messenger, input, serializer, log)
        {
        }

        public override string ToString() => Languages.SaveGraph;


        protected override async Task ExportAsync(string path)
        {
            await serializer.SerializeToFileAsync(graph, path);
        }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }

        private void OnGraphCreated(DataMessage<Graph2D<Vertex>> msg)
        {
            graph = msg.Value;
        }

        public override string ToString()
        {
            return Languages.SaveGraph;
        }

        public void RegisterHanlders(IMessenger messenger)
        {
            messenger.RegisterGraph(this, Tokens.Common, OnGraphCreated);
        }
    }
}
