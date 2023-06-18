namespace Pathfinding.App.Console.DataAccess
{
    internal interface IIdentityItem<TId> where TId : struct
    {
        TId Id { get; set; }
    }
}
