namespace Conditional.Interfaces
{
    public interface IConditionConstruction<T>
    {
        bool? IsCondition(T parametre);

        void ExecuteBody(T paramtre);
    }
}
