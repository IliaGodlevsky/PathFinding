using Pathfinding.Domain.Core;
using ReactiveUI;

namespace Pathfinding.ConsoleApp.Model
{
    internal sealed class GraphInfoModel : ReactiveObject
    {
        public int Id { get; set; }

        private string name;
        public string Name
        {
            get => name;
            set => this.RaiseAndSetIfChanged(ref name, value);
        }

        private Neighborhoods neighorhood;
        public Neighborhoods Neighborhood
        {
            get => neighorhood;
            set => this.RaiseAndSetIfChanged(ref neighorhood, value);
        }

        private SmoothLevels smoothLevel;
        public SmoothLevels SmoothLevel
        {
            get => smoothLevel;
            set => this.RaiseAndSetIfChanged(ref smoothLevel, value);
        }

        private int width;
        public int Width
        {
            get => width;
            set => this.RaiseAndSetIfChanged(ref width, value);
        }

        private int length;
        public int Length
        {
            get => length;
            set => this.RaiseAndSetIfChanged(ref length, value);
        }

        private int obstacles;
        public int Obstacles
        {
            get => obstacles;
            set => this.RaiseAndSetIfChanged(ref obstacles, value);
        }

        private GraphStatuses status;
        public GraphStatuses Status
        {
            get => status;
            set => this.RaiseAndSetIfChanged(ref status, value);
        }

        public object[] GetProperties()
        {
            return new object[] { Id, Name, Width, Length,
                Neighborhood, SmoothLevel, Obstacles, Status };
        }
    }
}
