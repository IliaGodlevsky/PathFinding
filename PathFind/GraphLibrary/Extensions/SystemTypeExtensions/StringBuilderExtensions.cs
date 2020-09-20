using System.Text;

namespace GraphLibrary.Extensions.SystemTypeExtensions
{
    public static class StringBuilderExtensions
    {
        public static StringBuilder AppendFormatLine(this StringBuilder builder,
            string format, params object[] args) 
            => builder.AppendFormat(format, args).AppendLine();
    }
}
