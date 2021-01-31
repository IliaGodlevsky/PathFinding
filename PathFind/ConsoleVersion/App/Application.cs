using ConsoleVersion.View.Interface;

namespace ConsoleVersion.App
{
    internal class Application : IApplication
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
