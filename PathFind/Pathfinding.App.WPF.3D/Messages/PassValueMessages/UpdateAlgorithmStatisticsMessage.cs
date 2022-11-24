using System;

namespace Pathfinding.App.WPF._3D.Messages.PassValueMessages
{
    internal sealed class UpdateAlgorithmStatisticsMessage
    {
        public Guid Id { get; }

        public string Time { get; }

        public int VisitedVertices { get; }

        public double PathCost { get; }

        public int PathLength { get; }

        public UpdateAlgorithmStatisticsMessage(Guid id, string time, int visitedVertices,
            int pathLength = default, double pathCost = default)
        {
            Id = id;
            Time = time;
            VisitedVertices = visitedVertices;
            PathCost = pathCost;
            PathLength = pathLength;
        }
    }
}