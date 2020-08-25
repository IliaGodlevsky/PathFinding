using ConsoleVersion.Graph;
using ConsoleVersion.Vertex;
using GraphLibrary;
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

        private static void WriteLine(int width)
        {
            for (int i = 0; i < width; i++)
                Console.Write(horizontalFrame);
        }

        private static void Show(ConsoleGraph graph)
        {
            for (int height = 0; height < graph.Height; height++)
            {
                string line = height < 10 ? space + verticalFrame : verticalFrame;
                Console.Write(height.ToString() + line);
                for (int width = 0; width < graph.Width; width++)
                {
                    ConsoleVertex vertex =
                        graph[width, height] as ConsoleVertex;
                    Console.Write(vertex.Text + bigSpace, vertex.Colour);
                    if (width == graph.Width - 1)
                        Console.Write(verticalFrame + height.ToString());
                    if (width != 0 && width % (graph.Width - 1) == 0)
                        Console.WriteLine();
                }
            }
        }

        public static void ShowGraph(ConsoleGraph graph)
        {
            if (graph == null)
                return;
            Console.WriteLine();
            WriteYCoordinate(graph.Width);
            WriteLine(graph.Width);
            Console.WriteLine();
            Show(graph);
            Console.Write(largeSpace);
            WriteLine(graph.Width);
            Console.WriteLine();
            WriteYCoordinate(graph.Width);
        }
    }
}
