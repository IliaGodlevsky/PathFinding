using Pathfinding.GraphLib.Core.Interface;

namespace Pathfinding.GraphLib.UnitTest.Realizations.TestFactories.Layers
{
    public sealed class ObstacleLayer : Graph2DLayer<bool>
    {
        private const bool X = true;
        private const bool O = false;

        protected override void Assign(IVertex vertex, bool value)
        {
            vertex.IsObstacle = value;
        }

        protected override bool[,] CreateMatrix()
        {
            return new bool[Constants.Width, Constants.Length]
            {
                {O,O,O,O,O,O,O,O,O,O,O,O,X,O,O,O,O,O,O,O},
                {O,O,O,O,O,O,O,O,O,X,O,O,O,O,O,X,O,O,O,X},
                {O,O,O,X,O,O,X,X,O,O,O,X,O,O,O,X,O,O,O,O},
                {O,X,X,O,O,O,O,O,O,X,O,X,O,O,X,X,O,O,O,O},
                {O,O,O,O,O,O,O,O,O,X,O,O,X,O,O,O,O,O,X,O},
                {O,X,O,X,O,O,O,O,O,O,X,O,O,O,O,O,O,O,O,O},
                {O,O,O,O,O,O,O,O,O,O,O,O,X,O,O,O,O,O,O,O},
                {O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O},
                {O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,X,O,O,O},
                {O,O,O,O,O,O,X,X,X,O,O,O,O,X,O,O,O,O,O,O},
                {O,X,O,O,O,O,O,O,O,O,O,O,O,O,X,O,O,O,O,O},
                {O,O,O,O,O,O,O,O,O,O,X,O,X,O,X,O,O,O,O,O},
                {O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O},
                {O,O,O,O,O,X,X,O,O,O,O,O,O,O,O,O,O,O,O,O},
                {O,O,X,O,O,O,O,O,X,O,O,O,X,O,O,O,O,O,O,X},
                {O,O,O,O,X,O,O,O,O,O,O,X,O,O,O,O,O,O,O,O},
                {O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O},
                {O,O,O,O,O,X,X,O,O,O,X,O,O,O,O,O,O,O,O,X},
                {O,X,O,O,O,O,O,O,O,X,O,X,O,O,O,X,O,O,X,O},
                {O,O,X,O,O,O,O,O,O,O,O,O,O,X,O,O,O,O,O,O},
                {O,O,O,O,O,O,O,O,O,X,O,O,O,X,O,O,O,O,O,O},
                {O,O,O,O,O,O,X,O,O,O,O,O,O,O,O,O,O,O,O,O},
                {O,O,O,O,O,O,O,O,X,O,O,O,O,O,O,O,X,X,O,O},
                {O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O},
                {O,O,O,X,O,O,O,O,O,O,O,O,O,O,X,O,O,O,O,O},
            };
        }
    }
}
