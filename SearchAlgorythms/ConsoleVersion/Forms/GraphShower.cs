using SearchAlgorythms.Graph;
using SearchAlgorythms.Top;
using System;
using System.Drawing;
using Console = Colorful.Console;

namespace ConsoleVersion.Forms
{
    public static class GraphShower
    {
        private static void WriteYCoordinate(ref ConsoleGraph graph)
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

        public static void ShowGraph(ref ConsoleGraph graph)
        {
            WriteYCoordinate(ref graph);
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
