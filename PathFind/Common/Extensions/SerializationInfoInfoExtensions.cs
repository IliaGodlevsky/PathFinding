using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Common.Extensions
{
    public static class SerializationInfoInfoExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Add<T>(this SerializationInfo info, string name, T item)
            where T : class
        {
            info.AddValue(name, item, typeof(T));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Get<T>(this SerializationInfo info, string name)
        {
            return (T)info.GetValue(name, typeof(T));
        }
    }
}
