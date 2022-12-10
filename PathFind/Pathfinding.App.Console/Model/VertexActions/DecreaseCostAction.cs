namespace Pathfinding.App.Console.Model.VertexActions
{
    internal sealed class DecreaseCostAction : ChangeCostAction
    {
        protected override int Increment => -1;
    }
}
