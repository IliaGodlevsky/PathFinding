using Common.Attrbiutes;

namespace Algorithm.Factory.Attrbiutes
{
    internal sealed class WaveGroupAttribute : GroupAttribute
    {
        public override bool Equals(object obj)
        {
            return obj is WaveGroupAttribute;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
