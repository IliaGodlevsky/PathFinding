using Pathfinding.App.Console.DataAccess.Entities;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;

namespace Pathfinding.App.Console.DataAccess.Repos
{
    internal interface IGraphRepository
    {
        GraphEntity GetGraph(int graphId);

        GraphEntity AddGraph(GraphEntity graph);

        bool DeleteGraph(int graphId);

        bool Update(GraphEntity graph);

        IEnumerable<GraphEntity> GetAll();
    }
}
