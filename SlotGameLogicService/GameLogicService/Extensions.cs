using System.Text;

namespace GameLogicService
{
    static class stringExtension
    {
        /// <summary>
        /// Single Quart
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        static public string SQ(this string source)
        {
            return Quart(source, "'");
        }

        /// <summary>
        /// Double Quart
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        static public string DQ(this string source)
        {
            return Quart(source, @"""");
        }

        static string Quart(string source, string quart)
        {
            var builder = new StringBuilder();
            builder.Append(quart);
            builder.Append(source);
            builder.Append(quart);
            return builder.ToString();
        }

        static public int ParseInt(this string source)
        {
            return int.Parse(source);
        }
    }
}
