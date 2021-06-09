using Conditional.Interfaces;

namespace Conditional
{
    public sealed class NullConditionConstruction : IConditionConstruction<object>
    {
        public void ExecuteBody(object paramtre)
        {

        }

        public bool? IsCondition(object parametre)
        {
            return false;
        }
    }
}
