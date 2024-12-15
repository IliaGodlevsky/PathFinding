using Terminal.Gui;

namespace Pathfinding.ConsoleApp.View
{
    internal static class ColorConstants
    {
        public const Color BackgroundColor = Color.Black;
        public const Color RegularVertexColor = Color.DarkGray;
        public const Color SourceVertexColor = Color.BrightMagenta;
        public const Color TargetVertexColor = Color.BrightRed;
        public const Color TranstiVertexColor = Color.BrightGreen;
        public const Color ObstacleVertexColor = Color.Black;
        public const Color VisitedVertexColor = Color.Gray;
        public const Color EnqueuedVertexColor = Color.White;
        public const Color PathVertexColor = Color.Brown;
        public const Color CrossedPathColor = Color.Red;
    }
}
