using SearchAlgorythms.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleMenu menu = new ConsoleMenu();
            menu.ShowGraph();
            menu.ChooseStart();
            menu.ChooseEnd();
            Console.Clear();
            menu.ShowGraph();
            menu.Find();
            Console.Clear();
            menu.ShowGraph();
            Console.ReadKey();
        }
    }
}
