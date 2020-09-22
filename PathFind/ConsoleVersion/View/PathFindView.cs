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
            var enums = Enum.GetValues(typeof(Algorithms)).Cast<Algorithms>().ToList();

            for (int i = 0; i < enums.Count(); i++)
                algorithmList.AppendFormatLine(ConsoleVersionResources.MenuFormat, i, enums[i].GetDescription());            

            return algorithmList.ToString();
        }
    }
}
