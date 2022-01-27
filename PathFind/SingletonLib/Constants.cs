using System;

namespace SingletonLib
{
    internal static class Constants
    {
        private const string Message = "{0} has neither private nor protected parametreless constructor";

        public static string GetMessage(Type genericType)
        {
            return string.Format(Message, genericType.Name);
        }
    }
}
