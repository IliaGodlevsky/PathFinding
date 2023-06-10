//using Pathfinding.App.Console.Localization;

//namespace Pathfinding.App.Console.Model.Notes
//{
//    internal class HistoryNote : StatisticsNote
//    {
//        private readonly string NoteFormat = Languages.PathfindingResultFormat;

//        public HistoryNote()
//        {

//        }

//        public HistoryNote(StatisticsNote note)
//        {
//            AlgorithmName = note.AlgorithmName;
//            Elapsed = note.Elapsed;
//            VisitedVertices = note.VisitedVertices;
//        }

//        public int Steps { get; set; }

//        public double Cost { get; set; }

//        public override string ToString()
//        {
//            int costValue = (int)Cost;
//            string result = base.ToString();
//            string steps = Steps.ToString().PadRight(IntPadding);
//            string cost = costValue.ToString().PadRight(IntPadding);
//            return string.Format(NoteFormat, result, steps, cost);
//        }
//    }
//}
