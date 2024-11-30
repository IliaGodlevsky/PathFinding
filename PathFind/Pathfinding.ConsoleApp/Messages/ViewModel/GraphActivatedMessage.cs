using Pathfinding.ConsoleApp.Model;
using Pathfinding.Service.Interface.Models.Read;
using System;

namespace Pathfinding.ConsoleApp.Messages.ViewModel
{
    internal sealed record class GraphActivatedMessage(GraphModel<GraphVertexModel> Graph) : IMayBeAsync
    {
        public Action Signal { get; set; }
    }
}
