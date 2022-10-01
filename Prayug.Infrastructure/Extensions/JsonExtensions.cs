using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Prayug.Infrastructure.Extensions
{
    public static class JsonExtensions
    {
        /// <summary>
        /// Converts given object to JSON string.
        /// </summary>
        /// <returns></returns>
        public static string ToJsonString<T>(this T obj, bool indented = false)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = indented
            };

            return JsonSerializer.Serialize<T>(obj, options);
        }
        public static T ToStringToObject<T>(this string obj, bool indented = false)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = indented
            };

            return JsonSerializer.Deserialize<T>(obj, options);
        }
    }
}
