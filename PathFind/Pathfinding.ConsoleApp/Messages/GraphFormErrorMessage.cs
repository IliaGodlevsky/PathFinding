namespace Pathfinding.ConsoleApp.Messages
{
    internal sealed class GraphFormErrorMessage
    {
        public string ErrorMessage { get; }

        public GraphFormErrorMessage(string message)
        {
            ErrorMessage = message;
        }
    }
}
