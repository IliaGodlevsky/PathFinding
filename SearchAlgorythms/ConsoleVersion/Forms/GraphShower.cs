using SearchAlgorythms.Graph;
using SearchAlgorythms.Top;
using Console = Colorful.Console;

namespace ConsoleVersion.Forms
{
    public static class GraphShower
    {
        private static void ShowGraphParams(ConsoleGraph graph)
        {
            Console.WriteLine("Percent of obstacles: " + graph.GetObstaclePercent());
            Console.WriteLine("Graph width: " + graph.GetWidth());
            Console.WriteLine("Graph height: " + graph.GetHeight());
        }

        private static void WriteYCoordinate(ConsoleGraph graph)
        {
            Console.Write("   ");
            for (int i = 0; i < graph.GetWidth(); i++)
            {
                if (i < 10)
                    Console.Write(i.ToString() + "  ");
                else
                    Console.Write(i.ToString() + " ");
            }
            Console.WriteLine();
            Console.Write("  ");
            for (int i = 0; i < graph.GetWidth(); i++)
                Console.Write("___");
            Console.WriteLine();
        }

        public static void ShowGraph(ConsoleGraph graph)
        {
            ShowGraphParams(graph);
            WriteYCoordinate(graph);
            string line;
            for (int height = 0; height < graph.GetHeight(); height++)
            {
                if (height < 10)
                    line = " |";
                else
                    line = "|";
                Console.Write(height.ToString() + line);
                for (int width = 0; width <graph.GetWidth(); width++) 
                {
                    ConsoleGraphTop top = graph[width, height] as ConsoleGraphTop;
                    Console.Write(top.Text + "  ", top.Colour);
                    if (width != 0 && width % (graph.GetWidth() - 1) == 0) 
                        Console.Write("\n");
                }
            }
        }
    }
}
