global using Terminal = System.Console;

using Pathfinding.App.Console.DependencyInjection.Registrations;
using System.ComponentModel;

namespace System.Runtime.CompilerServices
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public record IsExternalInit;
}

internal class Program
{
    private static void Main(string[] args)
    {
        using (var app = new Application())
        {
            app.ApplyComponents();
            app.Run();
        }
    }
}