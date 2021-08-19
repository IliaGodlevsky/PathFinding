namespace Common.Interface
{
    public interface IConditionConstruction<T>
    {
        bool IsCondition(T param);

        void Execute(T param);
    }
}
