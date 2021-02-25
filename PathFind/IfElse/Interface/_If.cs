namespace IfElse.Interface
{
    public interface _If<T>
    {
        bool? IsCondition(T parametre);

        void ExecuteBody(T paramtre);
    }
}
