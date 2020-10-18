using Dynamitey;
using GraphLibrary.DTO;
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
            {
                foreach (var inter in self.GetType().GetInterfaces())
                {
                    var interfaceProperties = inter.GetProperties().GetMarked<TMark>();
                    properties.AddRange(interfaceProperties.Cast<PropertyInfo>());
                }
            }
            var objectProps = self.GetType().GetProperties().GetMarked<TMark>();
            properties.AddRange(objectProps.Cast<PropertyInfo>());
            properties = properties.DistinctBy(prop => prop.Name).ToList();
            return properties;
        }

        public static void InitializeByDto<TSource>(this TSource self, Dto<TSource> dto)
        {
            foreach (var property in self.GetType().GetProperties())
            {
                var dictionary = (Dictionary<string, object>)dto;
                if (dictionary.ContainsKey(property.Name))
                {
                    var result = Dynamic.InvokeGet(dto, property.Name);
                    property.SetValue(self, result);
                }
            }
        }
    }
}
