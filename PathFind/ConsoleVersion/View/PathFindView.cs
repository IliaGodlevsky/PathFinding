using Algorithm.AlgorithmCreating;
using Common.Extensions;
using ConsoleVersion.View.Interface;
using ConsoleVersion.ViewModel;
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

            Model.AlgorithmKeyInputMessage = AlgorithmsList + ConsoleVersionResources.ChooseAlrorithm;
            Model.StartVertexInputMessage = "\n" + ConsoleVersionResources.StartVertexPointInputMsg;
            Model.EndVertexInputMessage = ConsoleVersionResources.EndVertexCoordinateInputMsg;
        }

        public void Start()
        {
            Model.FindPath();
        }

        private string AlgorithmsList
        {
            get
            {
                var algorithmList = new StringBuilder("\n");
                var algorithmKeys = AlgorithmFactory.GetAlgorithmKeys().ToArray();

                for (int i = 0; i < algorithmKeys.Length; i++)
                {
                    string format = ConsoleVersionResources.MenuFormat;
                    algorithmList.AppendFormatLine(format, i + 1, algorithmKeys[i]);
                }

                return algorithmList.ToString();
            }
        }
    }
}
