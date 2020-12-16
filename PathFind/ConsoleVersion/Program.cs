using ConsoleVersion.View;

namespace ConsoleVersion
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var view = new MainView();
            view.Start();
        }
    }
}
