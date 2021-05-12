using Common.Extensions;
using NullObject.Attributes;

namespace NullObject.Extensions
{
    public static class ObjectExtensions
    {
        public static bool IsNullObject(this object self)
        {
            if (self == null)
            {
                return true;
            }

            return self.GetType().GetAttributeOrNull<NullAttribute>() != null;
        }
    }
}
