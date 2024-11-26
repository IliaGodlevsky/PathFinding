using Pathfinding.Domain.Core;

namespace Pathfinding.ConsoleApp.Messages.ViewModel
{
    internal sealed record class GraphStateChangedMessage(int Id, GraphStatuses Status);
}
