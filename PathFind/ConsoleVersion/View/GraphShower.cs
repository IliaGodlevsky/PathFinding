using ConsoleVersion.Model.Vertex;
using GraphLibrary.Vertex.Interface;
using GraphLibrary.ViewModel.Interface;
using System.Text;
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

        private static string DrawAbscissa(int width)
        {
            var abscissa = new StringBuilder();
            abscissa.Append(largeSpace);
            for (int i = 0; i < width; i++)
                abscissa.Append(i + (i < OFFSET_ARRAY_INDEX ? bigSpace : space));
            abscissa.Append("\n" + largeSpace);
            return abscissa.ToString();
        }

        private static string WriteHorizontalFrame(int width)
        {
            var frame = new StringBuilder();
            for (int i = 0; i < width; i++)
                frame.Append(horizontalFrame);
            frame.Append("\n");
            return frame.ToString();
        }

        private static string DrawOrdinate(int height, 
            TableSide tableSide = TableSide.Left)
        {
            string line;
            if (tableSide == TableSide.Right)
                line = verticalFrame + height + "\n";
            else if (height < OFFSET_ARRAY_INDEX)
                line = height + space + verticalFrame;
            else
                line = height + verticalFrame;
            return line;
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
                Console.Write(DrawOrdinate(height));
                for (int width = 0; width < model.Graph.Width; width++)
                {
                    ShowVertex(model.Graph[width, height]);
                    if (IsEndOfRow(width, model))
                        Console.Write(DrawOrdinate(height, TableSide.Right));
                }
            }
        }

        private static void ShowGraphWithFrames(IMainModel model)
        {
            if (model.Graph == null)
                return;
            int width = model.Graph.Width;
            Console.Write(DrawAbscissa(width));
            Console.Write(WriteHorizontalFrame(width));
            ShowGraph(model);
            Console.Write(largeSpace);
            Console.Write(WriteHorizontalFrame(width));
            Console.Write(DrawAbscissa(width));
        }

        public static void DisplayGraph(IMainModel model)
        {
            if (model == null)
                return;
            Console.Clear();
            Console.WriteLine(model.GraphParametres);
            ShowGraphWithFrames(model);
            Console.WriteLine(model.Statistics);
        }
    }
}
