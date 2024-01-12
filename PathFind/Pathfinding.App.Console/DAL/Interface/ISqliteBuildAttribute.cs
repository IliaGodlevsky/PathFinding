namespace Pathfinding.App.Console.DAL.Interface
{
    internal interface ISqliteBuildAttribute
    {
        int Order { get; }

        string Text { get; }
    }
}
