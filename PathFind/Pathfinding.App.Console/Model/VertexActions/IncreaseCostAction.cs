namespace Pathfinding.App.Console.Model.VertexActions
{
    internal sealed class IncreaseCostAction : ChangeCostAction
    {
        protected override int Increment => 1;
    }
}
