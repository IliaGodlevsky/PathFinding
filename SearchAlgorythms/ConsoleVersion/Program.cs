using ConsoleVersion.Forms;
using System;

namespace ConsoleVersion
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleMenu menu = new ConsoleMenu();
            menu.Load();
            //menu.CreateGraph();
            menu.ShowGraph();
            menu.ChooseStart();
            menu.ChooseEnd();
            Console.Clear();
            menu.ShowGraph();
            menu.Find();
            Console.Clear();
            menu.ShowGraph();
            menu.ShowStat();
            Console.ReadKey();
            menu.Refresh();
            Console.Clear();
            menu.ShowGraph();
            menu.Save();
            Console.ReadKey();
        }
    }
}
