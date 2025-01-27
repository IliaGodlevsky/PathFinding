namespace Pathfinding.Shared.Interface
{
    public interface ICloneable<T>
        where T : ICloneable<T>
    {
        T DeepClone();
    }
}
