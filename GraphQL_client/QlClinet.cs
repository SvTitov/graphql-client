using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GraphQLClient
{
    public class QlClinet
    {
        readonly string _url;

        public QlClinet(string url)
        {
            this._url = url;
        }

        public async Task<T> ExecuteQuery<T>(string query, string param = null) 
        {
            var data = await InternalExecute(query, param);

            return JsonConvert.DeserializeObject<T>(data);
        }


        public async Task<dynamic> ExecuteQuery(string query, string param = null)
        {
            var data = await InternalExecute(query, param);

            return JsonConvert.DeserializeObject<dynamic>(data);
        }

        private async Task<string> InternalExecute(string query, string param)
        {
            HttpClient client = new HttpClient();
            var paramDictionary = new Dictionary<string, string>
            {
                {"query", query}
            };
            if (!string.IsNullOrEmpty(param))
                paramDictionary.Add("variables", param);

            var content = new FormUrlEncodedContent(paramDictionary);
            var result = await client.PostAsync(_url, content);
            var data = JObject.Parse(await result.Content.ReadAsStringAsync());

            return data["data"].ToString();
        }
    }
}
