using Pathfinding.App.Console.Model;

namespace Pathfinding.App.Console
{
    internal static class MessagesTexts
    {
        private static readonly string AnswerOptions = $"<{string.Join("/", Answer.Answers)}>";

        public static readonly string ApplyVisualizationMsg = $"Apply visualization {AnswerOptions}?: ";
        public static readonly string ExitAppMsg = $"Do you want to exit {AnswerOptions}?: ";
        public static readonly string ApplyHistoryMsg = $"Apply history recording {AnswerOptions}?: ";
        public static readonly string ApplyStatisticsMsg = $"Apply statistics {AnswerOptions}?: ";

        public const string SourceAndTargetInputMsg = "Input source and target vertices: ";
        public const string PathfindingStatisticsFormat = "Steps: {0}  Path cost: {1}  Visited: {2}";
        public const string InProcessStatisticsFormat = "Visited: {0}";
        public const string SourceVertexChoiceMsg = "Choose source vertex: ";
        public const string TargetVertexChoiceMsg = "Choose target vertex: ";
        public const string IntermediateVertexChoiceMsg = "Choose intermediate vertex: ";
        public const string IntermediateToReplaceMsg = "Choose intermedinates to replace: ";
        public const string PlaceToPutIntermediateMsg = "Choose place where to put intermediate: ";
        public const string AlgorithmChoiceMsg = "Choose algorithm: ";
        public const string GraphAssembleChoiceMsg = "Choose graph assemble: ";
        public const string DelayTimeInputMsg = "Input animation speed delay time (milliseconds): ";
        public const string GraphHeightInputMsg = "Input height of graph: ";
        public const string GraphWidthInputMsg = "Input width of graph: ";
        public const string RangeLowerValueInputMsg = "Input lower value of range: ";
        public const string RangeUpperValueInputMsg = "Input upper value of range: ";
        public const string CouldntFindPathMsg = "Couldn't find path";
        public const string ObstaclePercentInputMsg = "Input percent of obstacles: ";
        public const string MenuOptionChoiceMsg = "Choose menu option: ";
        public const string XCoordinateInputMsg = "Input X coordinate of vertex: ";
        public const string YCoordinateInputMsg = "Input Y coordinate of vertex: ";
        public const string PathfindingRangeFirstlyMsg = "Firstly choose pathfinding range";
        public const string VertexCostInputMsg = "Input vertex cost: ";
        public const string NumberOfTransitVerticesInputMsg = "Input number of intermediates vertices: ";
        public const string NumberOfIntermediatesVerticesToReplaceMsg = "Input number of intermediates to replace: ";
        public const string OutOfRangeMsg = "Value is out of range. Try again: ";
        public const string BadInputMsg = "Bad value input. Try again: ";
        public const string InputPathMsg = "Input path: ";
        public const string Quit = "Quit";

        public const string OperationIsNotSupported = "Operation is not supported";
        public const string AssebleIsNotChosenMsg = "Choose assemble for graph";
        public const string GraphSizeIsNotSetMsg = "Set graph size";
        public const string GraphIsNotCreatedMsg = "Graph is not created";
        public const string NoAlgorithmChosenMsg = "Choose algorithm to start pathfinding";
        public const string NoIntermediatesChosenMsg = "No intermediate vertices were chosen";
        public const string NoPathfindingRangeMsg = "Choose pathfinding range";
        public const string VisualizationIsNotAppliedMsg = "Pathfinding visualization is not applied";
        public const string NoVerticesToChooseAsRangeMsg = "No available vertices to include in range";
        public const string PathfindingRangeWasSetMsg = "Source and target vertives were set";
        public const string NoAlgorithmsWereStartedMsg = "No algorithms were started";
        public const string HistoryWasNotApplied = "Algorithms history recording was not applied";
    }
}