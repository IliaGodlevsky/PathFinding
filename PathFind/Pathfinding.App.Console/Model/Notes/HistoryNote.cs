using Pathfinding.App.Console.Extensions;

namespace Pathfinding.App.Console.Model.Notes
{
    internal class HistoryNote : StatisticsNote
    {
        public int Steps { get; set; }

        public int Cost { get; set; }

        public override string ToString()
        {
            string result = base.ToString();
            string steps = Steps.ToString().PadRight(IntPadding - Steps.GetDigitsNumber());
            string cost = Cost.ToString().PadRight(IntPadding - Cost.GetDigitsNumber());
            return $"{result}\tSteps: {steps}\tCost: {cost}";
        }
    }
}
