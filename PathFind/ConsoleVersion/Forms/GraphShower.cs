using SearchAlgorythms.Graph;
using SearchAlgorythms.Top;
using Console = Colorful.Console;

namespace ConsoleVersion.Forms
{
    public static class GraphShower
    {
        private static void ShowGraphParams(ConsoleGraph graph)
        {
            Console.WriteLine("Percent of obstacles: " + graph.ObstaclePercent);
            Console.WriteLine("Graph width: " + graph.Width);
            Console.WriteLine("Graph height: " + graph.Height);
        }

        private static void WriteYCoordinate(int width)
        {
            Console.Write("   ");
            for (int i = 0; i < width; i++)
            {
                if (i < 10)
                    Console.Write(i.ToString() + "  ");
                else
                    Console.Write(i.ToString() + " ");
            }
            Console.WriteLine();
            Console.Write("   ");           
        }

        private static void WriteLine(int width)
        {
            for (int i = 0; i < width; i++)
                Console.Write("---");
        }

        public static void ShowGraph(ConsoleGraph graph)
        {
            ShowGraphParams(graph);
            WriteYCoordinate(graph.Width);
            WriteLine(graph.Width);
            Console.WriteLine();
            string line;
            for (int height = 0; height < graph.Height; height++)
            {
                if (height < 10)
                    line = " |";
                else
                    line = "|";
                Console.Write(height.ToString() + line);
                for (int width = 0; width < graph.Width; width++) 
                {
                    ConsoleVertex top = graph[width, height] as ConsoleVertex;
                    Console.Write(top.Text + "  ", top.Colour);
                    if (width == graph.Width - 1)
                        Console.Write("|" + height.ToString());
                    if (width != 0 && width % (graph.Width- 1) == 0) 
                        Console.Write("\n");
                }
            }
            Console.Write("   ");
            WriteLine(graph.Width);
            Console.WriteLine();
            WriteYCoordinate(graph.Width);
        }
    }
}
