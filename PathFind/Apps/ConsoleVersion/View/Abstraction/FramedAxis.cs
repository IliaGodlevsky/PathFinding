namespace ConsoleVersion.View.Abstraction
{
    /// <summary>
    /// An abstract class, that provides methods for
    /// generating framed axis of a graph field
    /// </summary>
    internal abstract class FramedAxis
    {
        public abstract string GetFramedAxis();

        protected abstract string GetOffset();

        protected const char Space = ' ';
    }
}
