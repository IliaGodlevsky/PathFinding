global using Terminal = System.Console;

using Pathfinding.App.Console.DependencyInjection.Registrations;

using (var app = new Application())
{
    app.ApplyComponents();
    app.Run();
}