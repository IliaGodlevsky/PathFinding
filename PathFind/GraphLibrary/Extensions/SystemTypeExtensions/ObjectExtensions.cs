using Dynamitey;
using GraphLibrary.Attributes;
using GraphLibrary.Info;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GraphLibrary.Extensions.SystemTypeExtensions
{
    public static class ObjectExtensions
    {
        public static IEnumerable<PropertyInfo> GetMarkedProperties<TMark>(this object self, bool watchInterface)
            where TMark : Attribute
        {
            var properties = new List<PropertyInfo>();
            if (watchInterface)
                properties.AddRange(self.GetMarkedInterfaceProperties<TMark>());
            properties.AddRange(self.GetMarkedProperties<TMark>());
            properties = properties.DistinctBy(prop => prop.Name).ToList();
            return properties;
        }

        private static IEnumerable<PropertyInfo> GetMarkedInterfaceProperties<TMark>(this object self)
            where TMark : Attribute
        {
            foreach (var Interface in self.GetType().GetInterfaces())            
                foreach (var property in GetMarked<TMark>(Interface.GetProperties()))
                    yield return property;
        }

        private static IEnumerable<PropertyInfo> GetMarkedProperties<TMark>(this object self)
        {
            var properties = self.GetType().GetProperties();
            return GetMarked<TMark>(properties);
        }

        private static IEnumerable<PropertyInfo> GetMarked<TMark>(IEnumerable<PropertyInfo> properties)
        {
            foreach (var property in properties)
            {
                var attribute = property.GetCustomAttribute(typeof(TMark), inherit: true);
                if (attribute != null)
                    yield return property;
            }
        }

        public static void InitializeByInfo<TSource>(this TSource self, Info<TSource> info)
        {
            var markedProperties = self.GetMarkedProperties<InfoMemberAttribute>(true);
            foreach (var property in markedProperties)
            {
                var dictionary = (Dictionary<string, object>)info;
                if (dictionary.ContainsKey(property.Name))
                {
                    var result = Dynamic.InvokeGet(info, property.Name);
                    property.SetValue(self, result);
                }
            }
        }
    }
}
