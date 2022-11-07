namespace Shared.Executable
{
    public interface IExecutable<T>
    {
        void Execute(T obj);
    }
}
