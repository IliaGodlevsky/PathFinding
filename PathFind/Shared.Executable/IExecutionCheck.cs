namespace Shared.Executable
{
    public interface IExecutionCheck<T>
    {
        bool CanExecute(T obj);
    }
}
