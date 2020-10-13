using ConsoleVersion.View.Interface;
using ConsoleVersion.ViewModel;
using GraphLibrary.AlgorithmCreating;
using GraphLibrary.Extensions.SystemTypeExtensions;
using System;
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
            var algorithmKeys = AlgorithmFactory.GetAlgorithmKeys();
            for (int i = 0; i < algorithmKeys.Length; i++)             
                algorithmList.AppendFormatLine(ConsoleVersionResources.MenuFormat, i + 1, algorithmKeys[i]);            
            return algorithmList.ToString();
        }
    }
}
