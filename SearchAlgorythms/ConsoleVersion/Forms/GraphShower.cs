using SearchAlgorythms.Graph;
using SearchAlgorythms.Top;
using System;
using Console = Colorful.Console;

namespace ConsoleVersion.Forms
{
    public static class GraphShower
    {
        private static void WriteYCoordinate(ref ConsoleGraph graph)
        {
            Console.Write("   ");
            for (int i = 0; i < graph.GetHeight(); i++)
            {
                if (i < 10)
                    Console.Write(i.ToString() + "  ");
                else
                    Console.Write(i.ToString() + " ");
            }
            Console.WriteLine();
            Console.Write("  ");
            for (int i = 0; i < graph.GetHeight(); i++)
                Console.Write("___");
            Console.WriteLine();
        }

        public static void ShowGraph(ref ConsoleGraph graph)
        {
            WriteYCoordinate(ref graph);
            string line;
            for (int width = 0; width <graph.GetWidth(); width++)
            {
                if (width < 10)
                    line = " |";
                else
                    line = "|";
                Console.Write(width.ToString() + line);
                for (int height = graph.GetHeight() - 1; height >= 0; height--) 
                {
                    ConsoleGraphTop top = graph[width, height] as ConsoleGraphTop;
                    Console.Write(top.Text + "  ", top.Colour);
                    if (height  == 0)
                        Console.Write("\n");
                }
            }
        }
    }
}
