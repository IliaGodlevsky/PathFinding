namespace Commands.Interfaces
{
    public interface IExecutionCheck<T>
    {
        bool CanExecute(T obj);
    }
}
