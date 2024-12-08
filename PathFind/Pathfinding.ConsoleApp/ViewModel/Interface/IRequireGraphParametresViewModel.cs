namespace Pathfinding.ConsoleApp.ViewModel.Interface
{
    internal interface IRequireGraphParametresViewModel
    {
        public int Width { get; set; }

        public int Length { get; set; }

        public int Obstacles { get; set; }
    }
}
