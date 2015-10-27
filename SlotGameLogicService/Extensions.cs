using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlotGameLogicService
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
    }
}
