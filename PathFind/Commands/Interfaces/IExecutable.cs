namespace Commands.Interfaces
{
    public interface IExecutable<T>
    {
        void Execute(T obj);
    }
}
