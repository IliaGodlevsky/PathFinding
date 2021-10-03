using WPFVersion3D.Enums;

namespace WPFVersion3D.Messages
{
    internal readonly struct UpdateAlgorithmStatisticsMessage
    {
        public int Index { get; }
        public string Time { get; }
        public int VisitedVertices { get; }
        public double PathCost { get; }
        public int PathLength { get; }
        public AlgorithmStatus Status { get; }

        public UpdateAlgorithmStatisticsMessage(int index, 
            string time, 
            int visitedVertices, 
            AlgorithmStatus status = AlgorithmStatus.Started, 
            int pathLength = 0, 
            double pathCost = 0)
        {
            Index = index;
            Time = time;
            VisitedVertices = visitedVertices;
            PathCost = pathCost;
            PathLength = pathLength;
            Status = status;
        }
    }
}