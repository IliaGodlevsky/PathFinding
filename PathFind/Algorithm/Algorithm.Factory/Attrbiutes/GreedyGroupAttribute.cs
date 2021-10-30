using Common.Attrbiutes;

namespace Algorithm.Factory.Attrbiutes
{
    internal sealed class GreedyGroupAttribute : GroupAttribute
    {
        public GreedyGroupAttribute(int order = int.MaxValue) : base(order)
        {
        }

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
