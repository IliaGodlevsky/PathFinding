namespace Commands.Interfaces
{
    public interface ICanCheck<T>
    {
        bool CanExecute(T obj);
    }
}
