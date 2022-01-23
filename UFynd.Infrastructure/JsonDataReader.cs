using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using UFynd.Application.Contracts;
using UFynd.Application.Exceptions;
using UFynd.Common;
using UFynd.Common.Constants;

namespace UFynd.Infrastructure
{
    public class JsonDataReader<T> : IDataReader<T> where T : class
    {
        public async Task<IList<T>> LoadAsync(string path)
        {
            try
            {
                var response = new List<T>();
                if (string.IsNullOrEmpty(path))
                {
                    throw new UserFriendlyException(ErrorMessage.InvalidPath);
                }
                var json = await ReadFile(path);
                if (!string.IsNullOrEmpty(json))
                {
                    var jsonResponse = JsonDataSerializer.DeSerializeJson<IList<T>>(json);
                    response.AddRange(jsonResponse);
                }
                return response;
            }
            catch (FileNotFoundException ex)
            {
                throw new UserFriendlyException(ErrorMessage.InvalidPath, ex);
            }
            catch (JsonException ex)
            {
                throw new UserFriendlyException(ErrorMessage.InValidJson, ex);
            }

        }

        private IEnumerable<T> GetSerializedJson(string json)
        {
            var jsonResponse = JsonSerializer.Deserialize<IEnumerable<T>>(json, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            });

            return jsonResponse ?? new List<T>();
        }

        private async Task<string> ReadFile(string path)
        {
            using (var reader = new StreamReader(path))
            {
                var json = await reader.ReadToEndAsync();
                return json;
            }

        }
    }
}
