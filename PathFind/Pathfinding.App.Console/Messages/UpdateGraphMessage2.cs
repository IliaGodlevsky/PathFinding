using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pathfinding.App.Console.Messages
{
    internal class UpdateGraphMessage2
    {
        public Graph2D<Vertex> Graph { get; set; }
    }
}
