using System;
using System.Text;

namespace Common.Extensions
{
    public static class StringBuilderExtensions
    {
        public static StringBuilder AppendRepeat(this StringBuilder self,
            string value, int count)
        {
            for (int i = 0; i < count; i++)
            {
                self.Append(value);
            }
            return self;
        }

        public static StringBuilder AppendRepeat(this StringBuilder self,
            Func<int, string> value, int count)
        {
            for (int i = 0; i < count; i++)
            {
                self.Append(value(i));
            }
            return self;
        }

        public static StringBuilder AppendLineRepeat(this StringBuilder self,
            Func<int, string> value, int count)
        {
            for (int i = 0; i < count; i++)
            {
                self.AppendLine(value(i));
            }
            return self;
        }
    }
}
