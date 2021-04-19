using AssembleClassesLib.EventHandlers;

namespace AssembleClassesLib.Interface
{
    public interface INotifingAssembleClasses : IAssembleClasses
    {
        event AssembleClassesEventHandler OnClassesLoaded;
    }
}