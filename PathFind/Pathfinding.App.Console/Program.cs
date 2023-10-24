global using Terminal = System.Console;
using Pathfinding.App.Console.DependencyInjection.Registrations;
using System.Text;

internal class Program
{
    private static void Main(string[] args)
    {
        using (var app = new Application())
        {
            app.ApplyFeatures();
            app.Run(Encoding.UTF8);
        }
    }
}