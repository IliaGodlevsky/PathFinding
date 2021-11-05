using System;
using System.Text;

namespace Common.Extensions
{
    public static class StringBuilderExtensions
    {
        public static StringBuilder AppendMany(this StringBuilder self, string line, int times)
        {
            while (times-->0)
            {
                self.Append(line);
            }
            return self;
        }

        public static StringBuilder AppendMany(this StringBuilder self, Func<int, string> generator, int times)
        {
            for (int i = 0; i < times; i++)
            {
                string line = generator(i);
                self.Append(line);
            }
            return self;
        }

        public static StringBuilder AppendLineMany(this StringBuilder self, Func<int, string> generator, int times)
        {
            for (int i = 0; i < times; i++)
            {
                string line = generator(i);
                self.AppendLine(line);
            }
            return self;
        }
    }
}
