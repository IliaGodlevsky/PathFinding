using ConsoleVersion.Vertex;
using GraphLibrary.Model;
using GraphLibrary.Vertex;
using Console = Colorful.Console;

namespace ConsoleVersion.Forms
{

    internal static class GraphShower
    {
        private enum TableSide
        {
            Right,
            Left
        }

        private const int OFFSET_ARRAY_INDEX = 10;
        private const string space = " ";
        private const string bigSpace = "  ";
        private const string largeSpace = "   ";
        private const string horizontalFrame = "---";
        private const string verticalFrame = "|";

        private static void DrawAbscissa(int width)
        {
            Console.Write(largeSpace);
            for (int i = 0; i < width; i++)            
                Console.Write(i + (i < OFFSET_ARRAY_INDEX ? bigSpace : space));
            Console.Write("\n" + largeSpace);
        }

        private static void WriteHorizontalFrame(int width)
        {
            for (int i = 0; i < width; i++)
                Console.Write(horizontalFrame);
            Console.WriteLine();
        }

        private static bool IsNewLine(int width, IMainModel model)
        {
            return width != 0 && width % (model.Graph.Width - 1) == 0;
        }

        private static void DrawOrdinate(int height, TableSide tableSide = TableSide.Left)
        {
            string line;
            if (tableSide == TableSide.Right)
                line = verticalFrame + height;
            else if (height < OFFSET_ARRAY_INDEX)
                line = height + space + verticalFrame;
            else
                line = height + verticalFrame;
            Console.Write(line);
        }

        private static void ShowVertex(IVertex vertex)
        {
            ConsoleVertex vert = vertex as ConsoleVertex;
            Console.Write(vert.Cost + bigSpace, vert.Colour);
        }

        private static bool IsEndOfRow(int width, IMainModel model)
        {
            return width == model.Graph.Width - 1;
        }

        private static void ShowGraph(IMainModel model)
        {
            for (int height = 0; height < model.Graph.Height; height++)
            {
                DrawOrdinate(height);
                for (int width = 0; width < model.Graph.Width; width++)
                {
                    ShowVertex(model.Graph[width, height]);
                    if (IsEndOfRow(width, model))
                        DrawOrdinate(height, TableSide.Right);
                    if (IsNewLine(width, model))
                        Console.WriteLine();
                }
            }
        }

        private static void ShowGraphWithFrames(IMainModel model)
        {
            if (model.Graph == null)
                return;
            int width = model.Graph.Width;
            DrawAbscissa(width);
            WriteHorizontalFrame(width);
            ShowGraph(model);
            Console.Write(largeSpace);
            WriteHorizontalFrame(width);
            DrawAbscissa(width);
        }

        public static void DisplayGraph(IMainModel model)
        {
            Console.Clear();
            Console.WriteLine(model.GraphParametres);
            ShowGraphWithFrames(model);
            Console.WriteLine(model.Statistics);
        }
    }
}
