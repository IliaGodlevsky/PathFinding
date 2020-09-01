namespace GraphLibrary.PauseMaker
{
    public delegate void Pause();
    public interface IPauseProvider
    {
        Pause PauseEvent { get; set; }
        void Pause(int milliseconds);
    }
}
