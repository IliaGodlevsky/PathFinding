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
        public PathFindViewModel Model { get; }
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
            Model.FindPath();
        }

        private string GetAlgorithmsList()
        {
            var algorithmList = new StringBuilder("\n");
            Algorithms algo = Algorithms.AStarAlgorithm;
            var algoDescriptionList = algo.GetDescriptions().ToList();

            foreach (var item in algoDescriptionList)
            {
                int numberOf = algoDescriptionList.IndexOf(item) + 1;
                algorithmList.Append(string.Format(ConsoleVersionResources.MenuFormat, numberOf, item) + "\n");
            }

            return algorithmList.ToString();
        }
    }
}
