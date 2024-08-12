namespace Pathfinding.App.Console.Model
{
    public sealed class Pair<TKey, TValue>(TKey key, TValue value)
    {
        public TKey Key { get; } = key;

        public TValue Value { get; } = value;
    }
}
