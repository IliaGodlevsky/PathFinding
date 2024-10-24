using Pathfinding.Shared.Interface;

namespace Pathfinding.Shared.Test.Random
{
    public abstract class RandomTests
    {
        protected abstract IRandom Random { get; set; }

        protected IEnumerable<uint> GetRandomValues(int limit)
        {
            while (limit-- > 0)
            {
                yield return Random.NextUInt();
            }
        }

        public virtual void GetNextUInt_ShouldReturnValuesInTolerance(int limit, double tolerance)
        {
            var randomValues = GetRandomValues(limit).ToArray();

            var distinct = randomValues.Distinct().ToArray();

            Assert.That((randomValues.Length - distinct.Length) / randomValues.Length * 100 <= tolerance, Is.True,
                $"Generated unique values were out of the tolerance {tolerance} %");
        }
    }
}
