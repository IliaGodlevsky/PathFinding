using ConsoleVersion.Enums;
using ConsoleVersion.Forms;
using ConsoleVersion.InputClass;
using ConsoleVersion.ViewModel;
using GraphLibrary.Extensions;
using System.Linq;
using System.Text;
using Console = Colorful.Console;

namespace ConsoleVersion.View
{
    internal class MainView : IView
    {
        private const string newLine = "\n";
        private const string largeSpace = "   ";
        private const string tab = "\t";

        private delegate void MenuAction();
        private readonly MenuAction menuAction;
        private readonly MainViewModel mainModel;
        private readonly string menu;

        public MainView()
        {
            mainModel = new MainViewModel();

            menuAction += mainModel.PathFind;
            menuAction += mainModel.SaveGraph;
            menuAction += mainModel.LoadGraph;
            menuAction += mainModel.CreateNewGraph;
            menuAction += mainModel.ClearGraph;
            menuAction += mainModel.Reverse;
            menuAction += mainModel.ChangeVertexValue;
            
            menu = GetMenu();
        }

        private string GetMenu()
        {           
            var stringBuilder = new StringBuilder();
            var descriptions = ((MenuOption)default).GetDescriptions<MenuOption>();
            foreach (var item in descriptions)
            {               
                int numberOf = descriptions.IndexOf(item);
                stringBuilder.Append(numberOf.IsEven() ? newLine : largeSpace + tab);
                stringBuilder.Append(string.Format(Res.ShowFormat, numberOf, item));
            }
            return stringBuilder.ToString();
        }

        private MenuOption GetOption()
        {
            Console.WriteLine(menu);
            return Input.InputOption();
        }

        public void Start()
        {
            GraphShower.DisplayGraph(mainModel);
            var option = GetOption();
            while (option != MenuOption.Quit)
            {
                menuAction.
                    GetInvocationList().
                    ElementAt((byte)option).
                    DynamicInvoke();
                GraphShower.DisplayGraph(mainModel);
                option = GetOption();
            }
        }
    }
}
