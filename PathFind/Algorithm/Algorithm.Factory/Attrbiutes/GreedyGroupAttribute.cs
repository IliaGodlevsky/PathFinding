using Common.Attrbiutes;

namespace Algorithm.Factory.Attrbiutes
{
    internal sealed class GreedyGroupAttribute : GroupAttribute
    {
        public override bool Equals(object obj)
        {
            return obj is GreedyGroupAttribute;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
