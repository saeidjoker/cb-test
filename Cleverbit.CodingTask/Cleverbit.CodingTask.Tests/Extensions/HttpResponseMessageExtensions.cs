using System.Net.Http;
using Newtonsoft.Json;

namespace Cleverbit.CodingTask.Tests.Extensions {

    public static class HttpResponseMessageExtensions {
        public static T ParseAs<T>(this HttpResponseMessage response) {
            var jsonContent = response.Content.ReadAsStringAsync().Result;

            return JsonConvert.DeserializeObject<T>(jsonContent);
        }

        public static string ParseAsString(this HttpResponseMessage response) {
            return response.Content.ReadAsStringAsync().Result;
        }

        public static long ParseAsLong(this HttpResponseMessage response) {
            return long.Parse(response.ParseAsString());
        }

        public static int ParseAsInt(this HttpResponseMessage response) {
            return int.Parse(response.ParseAsString());
        }

        public static double ParseAsDouble(this HttpResponseMessage response) {
            return double.Parse(response.ParseAsString());
        }

        public static bool ParseAsBool(this HttpResponseMessage response) {
            return bool.Parse(response.ParseAsString());
        }
    }

}