using ConsoleVersion.View.Interface;
using ConsoleVersion.ViewModel;
using GraphLibrary.Enums;
using GraphLibrary.Extensions.SystemTypeExtensions;
using System;
using System.Linq;
using System.Text;

namespace ConsoleVersion.View
{
    internal class PathFindView : IView
    {
        public PathFindingViewModel Model { get; }
        public PathFindView(PathFindingViewModel model)
        {
            Model = model;
            Model.Messages = new Tuple<string, string, string>(
                "\n" + ConsoleVersionResources.StartPoint,
                ConsoleVersionResources.DestinationPoint,
                GetAlgorithmsList() + ConsoleVersionResources.ChooseAlrorithm);
        }

        public void Start()
        {
            Model.FindPath();
        }

        private string GetAlgorithmsList()
        {
            var algorithmList = new StringBuilder("\n");
            foreach (var item in Enum.GetValues(typeof(Algorithms)).Cast<Algorithms>())
            {
                algorithmList.AppendFormatLine(ConsoleVersionResources.MenuFormat, 
                    item.GetValue(), item.GetDescription());
            }
            return algorithmList.ToString();
        }
    }
}
