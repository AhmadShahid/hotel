using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace UFynd.Common
{
    public static class JsonDataSerializer
    {
        public static T DeSerializeJson<T>(string json) where T : class
        {
            var jsonResponse = JsonSerializer.Deserialize<T>(json, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            });

            return jsonResponse;
        }
    }
}
