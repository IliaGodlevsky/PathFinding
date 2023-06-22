namespace Pathfinding.App.Console.DataAccess
{
    internal interface IIdentityItem<TId>
    {
        TId Id { get; set; }
    }
}
