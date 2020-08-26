using ConsoleVersion.Vertex;
using GraphLibrary.Model;
using Console = Colorful.Console;

namespace ConsoleVersion.Forms
{
    public static class GraphShower
    {
        private const string space = " ";
        private const string bigSpace = "  ";
        private const string largeSpace = "   ";
        private const string horizontalFrame = "---";
        private const string verticalFrame = "|";

        private static void WriteYCoordinate(int width)
        {
            Console.Write(largeSpace);
            for (int i = 0; i < width; i++)            
                Console.Write(i.ToString() + (i < 10 ? bigSpace : space));           
            Console.WriteLine();
            Console.Write(largeSpace);
        }

        private static void WriteHorizontalFrame(int width)
        {
            for (int i = 0; i < width; i++)
                Console.Write(horizontalFrame);
        }

        private static bool IsNewLine(int width, IMainModel model)
        {
            return width != 0 && width % (model.Graph.Width - 1) == 0;
        }

        private static void WriteVerticalFrame(int height, bool reverse)
        {
            Console.Write(reverse ? verticalFrame + height.ToString() :
                height + (height < 10 ? space + verticalFrame : verticalFrame));           
        }

        private static void WriteVertex(ConsoleVertex vertex)
        {
            Console.Write(vertex.Text + bigSpace, vertex.Colour);
        }

        private static void ShowGraph(IMainModel model)
        {
            for (int height = 0; height < model.Graph.Height; height++)
            {
                WriteVerticalFrame(height, reverse: false);
                for (int width = 0; width < model.Graph.Width; width++)
                {
                    WriteVertex(model.Graph[width, height] as ConsoleVertex);
                    if (width == model.Graph.Width - 1)
                        WriteVerticalFrame(height, reverse: true);
                    if (IsNewLine(width, model))
                        Console.WriteLine();
                }
            }
        }

        public static void ShowGraphWithFrames(IMainModel model)
        {
            if (model.Graph == null)
                return;
            Console.WriteLine();
            WriteYCoordinate(model.Graph.Width);
            WriteHorizontalFrame(model.Graph.Width);
            Console.WriteLine();
            ShowGraph(model);
            Console.Write(largeSpace);
            WriteHorizontalFrame(model.Graph.Width);
            Console.WriteLine();
            WriteYCoordinate(model.Graph.Width);
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
