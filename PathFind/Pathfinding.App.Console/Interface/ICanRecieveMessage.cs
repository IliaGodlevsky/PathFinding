using CommunityToolkit.Mvvm.Messaging;

namespace Pathfinding.App.Console.Interface
{
    internal interface ICanReceiveMessage
    {
        void RegisterHandlers(IMessenger messenger);
    }
}
