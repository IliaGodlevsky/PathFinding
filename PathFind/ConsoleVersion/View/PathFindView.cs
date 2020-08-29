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
                "\n" + Res.StartPoint,
                Res.DestinationPoint,
                GetAlgorithmsList() + Res.ChooseAlrorithm);
        }

        public void Start()
        {
            Model.PathFind();
        }

        private string GetAlgorithmsList()
        {
            var stringBuilder = new StringBuilder("\n");

            foreach (var item in ((Algorithms)default).GetDescriptions<Algorithms>())
            {
                int numberOf = ((Algorithms)default).GetDescriptions<Algorithms>().IndexOf(item) + 1;
                stringBuilder.Append(string.Format(Res.ShowFormat, numberOf, item) + "\n");
            }

            return stringBuilder.ToString();
        }
    }
}
