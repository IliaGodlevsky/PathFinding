using Common.Extensions;
using NullObject.Attributes;

namespace NullObject.Extensions
{
    public static class ObjectExtensions
    {
        public static bool IsNullObject(this object self)
        {
            return self.GetType().GetAttribute<NullAttribute>() != null;
        }
    }
}
