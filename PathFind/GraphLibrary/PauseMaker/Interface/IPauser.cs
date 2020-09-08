namespace GraphLibrary.PauseMaker.Interface
{
    public delegate void Pause();
    public interface IPauseProvider
    {
        Pause PauseEvent { get; set; }
        void Pause();
    }
}
