using ConsoleVersion.View;

namespace ConsoleVersion.App
{
    internal class Application
    {
        public Application(MainView mainView)
        {
            this.mainView = mainView;
        }

        public void Run()
        {
            mainView.Start();
        }

        private readonly MainView mainView;
    }
}
