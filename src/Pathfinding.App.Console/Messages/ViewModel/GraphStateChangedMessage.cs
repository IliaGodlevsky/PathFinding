using Pathfinding.Domain.Core;

namespace Pathfinding.App.Console.Messages.ViewModel
{
    internal sealed record class GraphStateChangedMessage(int Id, GraphStatuses Status);
}
