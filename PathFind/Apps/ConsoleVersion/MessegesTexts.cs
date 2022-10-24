using ConsoleVersion.Model;

namespace ConsoleVersion
{
    internal static class MessagesTexts
    {
        private static readonly string AnswerOptions = $"<{string.Join("/", Answer.Answers)}>";

        public static readonly string ApplyVisualizationMsg = $"Apply visualization {AnswerOptions}?: ";
        public static readonly string ExitAppMsg = $"Do you want to exit {AnswerOptions}?: ";

        public const string SourceAndTargetInputMsg = "Input source and target vertices: ";
        public const string PathfindingStatisticsFormat = "Steps: {0}  Path cost: {1}  Visited: {2}";
        public const string SourceVertexChoiceMsg = "Choose source vertex: ";
        public const string TargetVertexChoiceMsg = "Choose target vertex: ";
        public const string IntermediateVertexChoiceMsg = "Choose intermediate vertex: ";
        public const string IntermediateToReplaceMsg = "Choose intermedinates to replace: ";
        public const string PlaceToPutIntermediateMsg = "Choose place where to put intermediate: ";
        public const string AlgorithmChoiceMsg = "Choose algorithm: ";
        public const string GraphAssembleChoiceMsg = "Choose graph assemble: ";
        public const string DelayTimeInputMsg = "Input delay time: ";
        public const string GraphHeightInputMsg = "Input height of graph: ";
        public const string GraphWidthInputMsg = "Input width of graph: ";
        public const string RangeLowerValueInputMsg = "Input lower value of range: ";
        public const string RangeUpperValueInputMsg = "Input upper value of range: ";
        public const string CouldntFindPathMsg = "Couldn't find path";
        public const string NoVerticesAsPathfindingRangeMsg = "No vertices to choose as end points";
        public const string ObstaclePercentInputMsg = "Input percent of obstacles: ";
        public const string MenuOptionChoiceMsg = "Choose menu option: ";
        public const string XCoordinateInputMsg = "Input X coordinate of vertex: ";
        public const string YCoordinateInputMsg = "Input Y coordinate of vertex: ";
        public const string PathfindingRangeFirstlyMsg = "Firstly choose endpoints";
        public const string VertexCostInputMsg = "Input vertex cost: ";
        public const string NotEnoughParamtres = "Not enough parametres to create graph";
        public const string NumberOfIntermediateVerticesInputMsg = "Input number of intermediates vertices: ";
        public const string NumberOfIntermediatesVerticesToReplaceMsg = "Input number of intermediates to replace: ";
        public const string OutOfRangeMsg = "Value is out of range. ";
        public const string BadInputMsg = "Bad input. Try again: ";
        public const string InputPathMsg = "Input path: ";
    }
}