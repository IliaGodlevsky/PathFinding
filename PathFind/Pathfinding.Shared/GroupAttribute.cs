using System;

namespace Pathfinding.Shared
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public sealed class GroupAttribute : Attribute
    {
        public object GroupToken { get; }

        public GroupAttribute(object groupToken)
        {
            GroupToken = groupToken;
        }
    }
}
