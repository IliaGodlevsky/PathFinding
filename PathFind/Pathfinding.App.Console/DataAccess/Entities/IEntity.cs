namespace Pathfinding.App.Console.DataAccess.Entities
{
    internal interface IEntity<TId>
    {
        TId Id { get; set; }
    }
}
