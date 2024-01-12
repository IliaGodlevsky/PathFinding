using System;

namespace Pathfinding.App.Console.DAL.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    internal sealed class IdentityAttribute : SqliteBuildAttribute
    {
        public IdentityAttribute(bool autoincrement)
            : base($"PRIMARY KEY {Autoincrement(autoincrement)}", 5)
        {

        }

        private static string Autoincrement(bool autoincrement)
        {
            return autoincrement ? "AUTOINCREMENT" : string.Empty;
        }
    }
}
