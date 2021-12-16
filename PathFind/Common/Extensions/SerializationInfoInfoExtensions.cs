using System.Runtime.Serialization;

namespace Common.Extensions
{
    public static class SerializationInfoInfoExtensions
    {
        public static void Add<T>(this SerializationInfo info, string name, T item)
            where T : class
        {
            info.AddValue(name, item, typeof(T));
        }

        public static T Get<T>(this SerializationInfo info, string name)
        {
            return (T)info.GetValue(name, typeof(T));
        }
    }
}
