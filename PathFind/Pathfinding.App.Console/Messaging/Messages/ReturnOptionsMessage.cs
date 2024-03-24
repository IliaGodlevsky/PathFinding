using Shared.Primitives.Extensions;

namespace Pathfinding.App.Console.Messaging.Messages
{
    internal class ReturnOptionsMessage
    {
        public ReturnOptions Options { get; }

        public ReturnOptionsMessage(ReturnOptions options)
        {
            Options = options;
        }
    }
}
