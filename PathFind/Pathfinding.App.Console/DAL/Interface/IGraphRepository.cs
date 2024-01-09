using Pathfinding.App.Console.DAL.Models.Entities;
using System.Collections.Generic;

namespace Pathfinding.App.Console.DAL.Interface
{
    internal interface IGraphParametresRepository
    {
        GraphEntity GetGraph(int graphId);

        GraphEntity AddGraph(GraphEntity graph);

        bool DeleteGraph(int graphId); // should be cascade

        bool Update(GraphEntity graph);

        IEnumerable<GraphEntity> GetAll();
    }
}
