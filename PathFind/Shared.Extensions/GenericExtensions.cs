namespace Shared.Extensions
{
    public static class GenericExtensions
    {
        public static bool IsOneOf<T>(this T self, params T[] objects)
        {
            for (int i = 0; i < objects.Length; i++)
            {
                if (!objects[i].Equals(self))
                {
                    return false;
                }
            }
            return true;
        }
    }
}