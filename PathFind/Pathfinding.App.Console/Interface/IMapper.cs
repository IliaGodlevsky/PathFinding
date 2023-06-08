namespace Pathfinding.App.Console.Interface
{
    internal interface IMapper<TFrom, TTo>
    {
        TTo Map(TFrom from);
    }
}
