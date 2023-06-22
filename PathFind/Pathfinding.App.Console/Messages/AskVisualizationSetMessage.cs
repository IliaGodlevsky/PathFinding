using Pathfinding.App.Console.Model;

namespace Pathfinding.App.Console.Messages
{
    internal class AskVisualizationSetMessage : AskMessage<VisualizationSet>
    {
        public long Id { get; set; }
    }
}
