using System.Reflection;

namespace GraphLibrary.Extensions.SystemTypeExtensions
{
    public static class ObjectExtensions
    {
        /// <summary>
        /// Initialize object properties with initializer's properties value. 
        /// Initializable object and initializer object must have properties with the same name and types
        /// </summary>
        /// <param name="self"></param>
        /// <param name="initializer"></param>
        public static void InitilizeBy(this object self, object initializer)
        {
            foreach (var selfProperty in self.GetType().GetProperties())
                foreach (var initializerProperty in initializer.GetType().GetProperties())
                    if (IsSuitableProperty(selfProperty, initializerProperty))
                        selfProperty.SetValue(self, initializerProperty.GetValue(initializer));
        }

        private static bool IsSuitableProperty(PropertyInfo first, PropertyInfo second)
        {
            return first.Name == second.Name && first.PropertyType == second.PropertyType;
        }
    }
}
