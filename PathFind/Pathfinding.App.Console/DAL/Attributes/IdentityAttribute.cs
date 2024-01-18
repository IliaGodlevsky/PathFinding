using System;

namespace Pathfinding.App.Console.DAL.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    internal sealed class IdentityAttribute(bool autoincrement = true) 
        : SqliteBuildAttribute($"PRIMARY KEY {Autoincrement(autoincrement)}", 6)
    {
        private static string Autoincrement(bool autoincrement)
        {
            return autoincrement ? "AUTOINCREMENT" : string.Empty;
        }
    }
}
