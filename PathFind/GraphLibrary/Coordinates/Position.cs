using System;

namespace GraphLibrary.Coordinates
{
    /// <summary>
    /// Cartesian coordinates of the vertex on the graph
    /// </summary>
    [Serializable]
    public struct Position
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override bool Equals(object pos)
        {
            if (pos is Position position)
                return position.X == X && position.Y == Y;
            return false;
        }

        public static bool operator ==(Position position1, Position position2)
        {
            return position1.Equals(position2);
        }

        public static bool operator !=(Position position1, Position position2)
        {
            return !position1.Equals(position2);
        }

        public override int GetHashCode()
        {
            return X ^ Y;
        }
    }
}
