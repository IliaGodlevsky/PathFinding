using Dynamitey;
using GraphLibrary.Attributes;
using GraphLibrary.DTO;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace GraphLibrary.Extensions.CustomTypeExtensions
{
    public static class DtoExtensions
    {
        public static void InitilizeByObject<TSource>(this Dto<TSource> self, TSource initializer)
        {
            foreach (var interf in initializer.GetType().GetInterfaces())
                self.InitilizeBy(initializer, interf.GetProperties());
            self.InitilizeBy(initializer, initializer.GetType().GetProperties());
        }

        public static void InitializeObject<TSource>(this Dto<TSource> self, TSource obj)
        {
            foreach (var interf in obj.GetType().GetInterfaces())
                self.Initialize(obj, interf.GetProperties());
            self.Initialize(obj, obj.GetType().GetProperties());
        }



        private static void InitilizeBy<TSource>(this Dto<TSource> self,
            TSource initializer, PropertyInfo[] properties)
        {
            Init<TSource>(properties, (property) =>
            {
                Dynamic.InvokeSet(self, property.Name, property.GetValue(initializer));
            });
        }

        private static void Initialize<TSource>(this Dto<TSource> self,
            TSource obj, PropertyInfo[] properties)
        {
            Init<TSource>(properties, (property) =>
            {
                var dictionary = (Dictionary<string, object>)self;
                if (dictionary.ContainsKey(property.Name))
                {
                    object result = Dynamic.InvokeGet(self, property.Name);
                    property.SetValue(obj, result);
                }
            });
        }

        private static void Init<TSource>(PropertyInfo[] properties,
            Action<PropertyInfo> settingAction)
        {
            foreach (var property in properties)
            {
                var attribute = property.GetCustomAttribute(typeof(DtoMemberAttribute), inherit: true);
                if (attribute != null)
                    settingAction(property);
            }
        }
    }
}
