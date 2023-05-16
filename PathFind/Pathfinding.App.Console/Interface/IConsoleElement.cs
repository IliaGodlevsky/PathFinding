namespace Pathfinding.App.Console.Interface
{
    internal interface IConsoleElement
    {
        string Content { get; set; }

        void Hover();

        void Display();
    }
}
