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
            return self
                .GetInterfaces()
                .Select(interf => interf.Name);
        }

        public static bool IsInterfaceImplemeted<Interface>(this Type self) where Interface : class
        {
            if (!typeof(Interface).IsInterface)
            {
                return false;
            }
            return self.GetInterfacesNames()
                .Contains(typeof(Interface).Name);
        }
    }
}
