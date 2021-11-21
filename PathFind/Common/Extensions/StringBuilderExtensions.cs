using System;
using System.Text;

namespace Common.Extensions
{
    public static class StringBuilderExtensions
    {
        /// <summary>
        /// Appends <paramref name="line"/> 
        /// for several <paramref name="times"/>
        /// </summary>
        /// <param name="self"></param>
        /// <param name="line">String to append</param>
        /// <param name="times">Number of times 
        /// the string must me appended</param>
        /// <returns>The same instance 
        /// of <see cref="StringBuilder"/></returns>
        public static StringBuilder AppendMany(this StringBuilder self,
            string line, int times)
        {
            while (times-- > 0)
            {
                self.Append(line);
            }
            return self;
        }

        /// <summary>
        /// Appends a string for several <paramref name="times"/> 
        /// using <paramref name="generator"/>
        /// as a function to generate strings
        /// </summary>
        /// <param name="self"></param>
        /// <param name="generator">A function, 
        /// that produces strings</param>
        /// <param name="times">Number of times 
        /// the string must be appended</param>
        /// <returns>The same instance of <see cref="StringBuilder"/></returns>
        public static StringBuilder AppendMany(this StringBuilder self,
            Func<int, string> generator, int times)
        {
            for (int i = 0; i < times; i++)
            {
                string line = generator(i);
                self.Append(line);
            }
            return self;
        }

        /// <summary>
        /// Appends with new line a string for several <paramref name="times"/> 
        /// using <paramref name="generator"/>
        /// as a function to generate strings
        /// </summary>
        /// <param name="self"></param>
        /// <param name="generator">A function, 
        /// that produces strings</param>
        /// <param name="times">Number of times 
        /// the string must be appended</param>
        /// <returns>The same instance of <see cref="StringBuilder"/></returns>
        public static StringBuilder AppendLineMany(this StringBuilder self,
            Func<int, string> generator, int times)
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
