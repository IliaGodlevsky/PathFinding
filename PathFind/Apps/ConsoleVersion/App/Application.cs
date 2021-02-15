using ConsoleVersion.View.Interface;

namespace ConsoleVersion.App
{
    internal class Application
    {
        public Application(IView mainView)
        {
            this.mainView = mainView;
        }

        public void Run()
        {
            mainView.Start();
        }

        private readonly IView mainView;
    }
}
