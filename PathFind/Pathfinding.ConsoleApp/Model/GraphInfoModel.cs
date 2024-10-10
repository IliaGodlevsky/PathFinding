using ReactiveUI;

namespace Pathfinding.ConsoleApp.Model
{
    internal sealed class GraphInfoModel : ReactiveObject
    {
        private int obstacles;
        private string name;

        public int Id { get; set; }

        public string Name
        {
            get => name;
            set => this.RaiseAndSetIfChanged(ref name, value);
        }

        public string Neighborhood { get; set; }

        public string SmoothLevel { get; set; }

        public int Width { get; set; }

        public int Length { get; set; }

        public int Obstacles
        {
            get => obstacles;
            set => this.RaiseAndSetIfChanged(ref obstacles, value);
        }
    }
}
