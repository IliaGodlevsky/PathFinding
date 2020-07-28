using GraphLibrary.Constants;
using System;

namespace GraphLibrary.Extensions.RandomExtension
{
    public static class RandomExtensions
    {
        public static string GetRandomVertexValue(this Random rand)
        {
            return (rand.Next(Const.MAX_VERTEX_VALUE) + Const.MIN_VERTEX_VALUE).ToString();
        }
    }
}
