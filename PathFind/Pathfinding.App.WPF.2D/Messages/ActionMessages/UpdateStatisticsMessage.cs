using System;

namespace Pathfinding.App.WPF._2D.Messages.ActionMessages
{
    internal sealed class UpdateStatisticsMessage
    {
        public Guid Id { get; }

        public string Time { get; }

        public int VisitedVertices { get; }

        public double PathCost { get; }

        public int PathLength { get; }

        public UpdateStatisticsMessage(Guid id, string time, int visitedVertices,
            int pathLength = 0, double pathCost = 0)
        {
            Id = id;
            Time = time;
            VisitedVertices = visitedVertices;
            PathCost = pathCost;
            PathLength = pathLength;
        }
    }
}