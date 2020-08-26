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
            {
                string str = i.ToString();
                str += i < 10 ? bigSpace : space;
                Console.Write(str);
            }
            Console.WriteLine();
            Console.Write(largeSpace);
        }

        private static void WriteHorizontalFrame(int width)
        {
            for (int i = 0; i < width; i++)
                Console.Write(horizontalFrame);
        }

        private static void ShowGraph(IMainModel model)
        {
            for (int height = 0; height < model.Graph.Height; height++)
            {
                string line = height < 10 ? space + verticalFrame : verticalFrame;
                Console.Write(height.ToString() + line);
                for (int width = 0; width < model.Graph.Width; width++)
                {
                    ConsoleVertex vertex =
                        model.Graph[width, height] as ConsoleVertex;
                    Console.Write(vertex.Text + bigSpace, vertex.Colour);
                    if (width == model.Graph.Width - 1)
                        Console.Write(verticalFrame + height.ToString());
                    if (width != 0 && width % (model.Graph.Width - 1) == 0)
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
