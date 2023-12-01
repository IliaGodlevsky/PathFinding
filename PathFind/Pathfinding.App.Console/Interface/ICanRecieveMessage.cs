using CommunityToolkit.Mvvm.Messaging;

namespace Pathfinding.App.Console.Interface
{
    internal interface ICanRecieveMessage
    {
        void RegisterHanlders(IMessenger messenger);
    }
}
