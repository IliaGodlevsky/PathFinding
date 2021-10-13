using System;

namespace WindowsFormsVersion
{
    class MessageTokens
    {
        public static Guid MainModel { get; }

        static MessageTokens()
        {
            MainModel = Guid.NewGuid();

        }
    }
}