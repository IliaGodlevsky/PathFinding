using ConsoleVersion.Graph;
using ConsoleVersion.Vertex;
using GraphLibrary;
using Console = Colorful.Console;

namespace ConsoleVersion.Forms
{
    public static class GraphShower
    {
        private static void WriteYCoordinate(int width)
        {
            Console.Write("   ");
            string str;
            for (int i = 0; i < width; i++)
            {
                str = i.ToString();
                str += i < 10 ? "  " : " ";
                Console.Write(str);
            }
            Console.WriteLine();
            Console.Write("   ");
        }

        private static void WriteLine(int width)
        {
            for (int i = 0; i < width; i++)
                Console.Write("---");
        }

        private static void Show(ConsoleGraph graph)
        {
            for (int height = 0; height < graph.Height; height++)
            {
                string line = height < 10 ? " |" : "|";
                Console.Write(height.ToString() + line);
                for (int width = 0; width < graph.Width; width++)
                {
                    ConsoleVertex vertex =
                        graph[width, height] as ConsoleVertex;
                    Console.Write(vertex.Text + "  ", vertex.Colour);
                    if (width == graph.Width - 1)
                        Console.Write("|" + height.ToString());
                    if (width != 0 && width % (graph.Width - 1) == 0)
                        Console.WriteLine();
                }
            }
        }

        public static void ShowGraph(ConsoleGraph graph)
        {
            if (graph == null)
                return;
            Console.WriteLine(GraphDataFormatter.
                GetFormattedData(graph, Res.GraphParametresFormat));
            Console.WriteLine();
            WriteYCoordinate(graph.Width);
            WriteLine(graph.Width);
            Console.WriteLine();
            Show(graph);
            Console.Write("   ");
            WriteLine(graph.Width);
            Console.WriteLine();
            WriteYCoordinate(graph.Width);
        }
    }
}
