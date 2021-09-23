using Autofac;
using ConsoleVersion.Configure;
using ConsoleVersion.Enums;
using ConsoleVersion.ValueInput;
using ConsoleVersion.ValueInput.Interface;
using ConsoleVersion.View.Interface;

namespace ConsoleVersion
{
    internal class Program
    {
        public static IValueInput<Answer> AnswerInput => EnumValueInput<Answer>.Instance;
        public static IValueInput<int> Input => Int32ValueInput.Instance;

        private static void Main(string[] args)
        {
            var container = ContainerConfigure.Configure();

            using (var scope = container.BeginLifetimeScope())
            {
                var mainView = scope.Resolve<IView>();
                mainView.Start();
            }
        }
    }
}