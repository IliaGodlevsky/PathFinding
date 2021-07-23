using NullObject.Attributes;
using System;
using System.Collections;
using System.Linq;

namespace NullObject.Extensions
{
    public static class ObjectExtensions
    {
        public static bool IsNull(this object self)
        {
            return self == null || Attribute.IsDefined(self.GetType(), typeof(NullAttribute));
        }
    }
}
