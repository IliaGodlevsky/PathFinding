using Pathfinding.App.Console.Extensions;

namespace Pathfinding.App.Console.Model.Notes
{
    internal class StatisticsNote
    {
        protected const int IntPadding = 10;

        public string AlgorithmName { get; set; }

        public string Time { get; set; } = string.Empty;

        public int VisitedVertices { get; set; }

        public override string ToString()
        {
            string algorithmName = AlgorithmName.PadRight(totalWidth: 25);
            int value = VisitedVertices;
            string visited = value.ToString().PadRight(IntPadding - value.GetDigitsNumber());
            return $"{algorithmName}\t{Time}\tVisited: {visited}";
        }
    }
}
