using Pathfinding.Service.Interface.Models.Read;
using System;

namespace Pathfinding.ConsoleApp.Messages.ViewModel
{
    internal sealed record class GraphUpdatedMessage(GraphInformationModel Model) : IMayBeAsync
    {
        public Action Signal { get; set; }
    }
}
