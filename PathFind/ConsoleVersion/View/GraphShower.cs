using ConsoleVersion.Vertex;
using GraphLibrary.Model;
using GraphLibrary.Vertex;
using Console = Colorful.Console;

namespace ConsoleVersion.Forms
{
    internal static class GraphShower
    {
        private const string space = " ";
        private const string bigSpace = "  ";
        private const string largeSpace = "   ";
        private const string horizontalFrame = "---";
        private const string verticalFrame = "|";

        private static void DrawAbscissa(int width)
        {
            Console.Write(largeSpace);
            for (int i = 0; i < width; i++)            
                Console.Write(i + (i < 10 ? bigSpace : space));
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

        private static void DrawOrdinate(int height, bool reverse)
        {
            Console.Write(reverse ? verticalFrame + height.ToString() :
                height + (height < 10 ? space + verticalFrame : verticalFrame));           
        }

        private static void ShowVertex(IVertex vertex)
        {
            ConsoleVertex vert = vertex as ConsoleVertex;
            Console.Write(vert.Cost + bigSpace, vert.Colour);
        }

        private static void ShowGraph(IMainModel model)
        {
            for (int height = 0; height < model.Graph.Height; height++)
            {
                DrawOrdinate(height, reverse: false);
                for (int width = 0; width < model.Graph.Width; width++)
                {
                    ShowVertex(model.Graph[width, height]);
                    if (width == model.Graph.Width - 1)
                        DrawOrdinate(height, reverse: true);
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
            Console.WriteLine(model?.Statistics);
        }
    }
}
