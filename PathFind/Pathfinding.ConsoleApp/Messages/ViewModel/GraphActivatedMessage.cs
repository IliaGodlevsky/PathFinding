﻿using Pathfinding.ConsoleApp.Model;
using Pathfinding.Service.Interface.Models.Read;

namespace Pathfinding.ConsoleApp.Messages.ViewModel
{
    internal sealed record class GraphActivatedMessage(GraphModel<GraphVertexModel> Graph);
}
