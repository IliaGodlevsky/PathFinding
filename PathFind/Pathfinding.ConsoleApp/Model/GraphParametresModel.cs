using ReactiveUI;

namespace Pathfinding.ConsoleApp.Model
{
    internal sealed class GraphParametresModel : ReactiveObject
    {
        private int obstacles;
        private string name;

        public int Id { get; set; }

        public string Name
        {
            get => name;
            set => this.RaiseAndSetIfChanged(ref name, value);
        }

        public int Width { get; set; }

        public int Length { get; set; }

        public int Obstacles
        {
            get=> obstacles;
            set => this.RaiseAndSetIfChanged(ref obstacles, value);
        }
    }
}
