using Algorithm.AlgorithmCreating;
using Common.Extensions;
using ConsoleVersion.View.Interface;
using ConsoleVersion.ViewModel;
using GraphLib.Coordinates;
using GraphLib.Graphs;
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

            var algorithmList = GetAlgorithmsList();

            Model.AlgorithmKeyInputMessage = algorithmList + ConsoleVersionResources.ChooseAlrorithm;
            Model.StartVertexInputMessage = "\n" + ConsoleVersionResources.StartPoint;
            Model.EndVertexInputMessage = ConsoleVersionResources.DestinationPoint;
        }

        public void Start()
        {
            Model.FindPath();
        }

        private string GetAlgorithmsList()
        {
            var algorithmList = new StringBuilder("\n");
            var algorithmKeys = AlgorithmFactory.AlgorithmKeys.ToArray();

            for (int i = 0; i < algorithmKeys.Length; i++)
            {
                algorithmList.AppendFormatLine(
                    ConsoleVersionResources.MenuFormat, 
                    i + 1, 
                    algorithmKeys[i]);
            }

            return algorithmList.ToString();
        }
    }
}
