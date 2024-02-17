using Pathfinding.App.Console.DAL.Models.Entities;
using System.Collections.Generic;

namespace Pathfinding.App.Console.DAL.Interface
{
    internal interface IGraphParametresRepository
    {
        int GetCount();

        GraphEntity Read(int graphId);

        GraphEntity Insert(GraphEntity graph);

        bool Delete(int graphId); // should be cascade

        bool Update(GraphEntity graph);

        IEnumerable<GraphEntity> GetAll();
    }
}
