using ConsoleVersion.ViewModel;
using GraphLibrary.AlgorithmEnum;
using GraphLibrary.Extensions;
using System;
using System.Text;

namespace ConsoleVersion.View
{
    internal class PathFindView : IView
    {
        public PathFindViewModel Model { get; set; }
        public PathFindView(PathFindViewModel model)
        {
            Model = model;
            Model.Messages = new Tuple<string, string, string>(
                "\n" + ConsoleVersionResources.StartPoint,
                ConsoleVersionResources.DestinationPoint,
                GetAlgorithmsList() + ConsoleVersionResources.ChooseAlrorithm);
        }

        public void Start()
        {
            Model.PathFind();
        }

        private string GetAlgorithmsList()
        {
            var algorithmList = new StringBuilder("\n");
            var algoDescriptionList = ((Algorithms)default).GetDescriptions();

            foreach (var item in algoDescriptionList)
            {
                int numberOf = algoDescriptionList.IndexOf(item) + 1;
                algorithmList.Append(string.Format(ConsoleVersionResources.MenuFormat, numberOf, item) + "\n");
            }

            return algorithmList.ToString();
        }
    }
}
