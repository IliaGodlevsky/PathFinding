using Pathfinding.App.Console.DAL.Models.Entities;
using System;
using System.Collections.Generic;

namespace Pathfinding.App.Console.DAL.Interface
{
    internal interface IStatisticsRepository
    {
        StatisticsEntity GetByAlgorithmRunId(int runId);

        IEnumerable<StatisticsEntity> GetByRunIds(IEnumerable<int> runIds);

        StatisticsEntity Insert(StatisticsEntity entity);
    }
}
