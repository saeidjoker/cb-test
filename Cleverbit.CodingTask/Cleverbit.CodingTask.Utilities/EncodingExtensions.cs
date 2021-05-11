using System;
using System.Text;

namespace Cleverbit.CodingTask.Utilities {

    public static class EncodingExtensions {
        public static string ToBase64(this string plainText) {
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);

            return System.Convert.ToBase64String(plainTextBytes);
        }

        public static string FromBase64(this string base64EncodedData) {
            var base64EncodedBytes = Convert.FromBase64String(base64EncodedData);

            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
    }

}