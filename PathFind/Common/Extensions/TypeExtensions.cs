using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Common.Extensions
{
    public static class TypeExtensions
    {
        public static Assembly GetAssembly(this Type self)
        {
            return Assembly.Load(self.Assembly.GetName());
        }

        public static IEnumerable<string> GetInterfacesNames(this Type self)
        {
            var interfaces = self.GetInterfaces();
            return interfaces.Select(interf => interf.Name);
        }
    }
}
