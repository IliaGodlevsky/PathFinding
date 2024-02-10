using Pathfinding.App.Console.DAL.Models.Entities;
using System;

namespace Pathfinding.App.Console.DAL.Interface
{
    internal interface IGraphStateRepository
    {
        GraphStateEntity Insert(GraphStateEntity entity);

        GraphStateEntity GetByRunId(int runId);
    }
}
