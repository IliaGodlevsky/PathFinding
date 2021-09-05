namespace Common.Interface
{
    public interface ICloneable<out T>
    {
        T Clone();
    }
}
