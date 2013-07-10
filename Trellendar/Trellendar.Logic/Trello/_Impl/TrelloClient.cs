using System;
using System.Collections.Generic;
using System.Net.Http;
using Trellendar.Exceptions;

namespace Trellendar.Trello._Impl
{
    public class TrelloClient : ITrelloClient
    {
        private readonly HttpClient _httpClient;

        public TrelloClient()
        {
            _httpClient = new HttpClient { BaseAddress = new Uri("https://api.trello.com/1/") };
        }

        public string Get(string resource, IDictionary<string, object> parameters = null)
        {
            if (resource == null)
            {
                throw new ArgumentNullException("resource");
            }

            IncludeKeyParameter(ref parameters);
            var requestUri = FormatRequestUri(resource, parameters);
            var response = _httpClient.GetAsync(requestUri).Result;
            
            if (!response.IsSuccessStatusCode)
            {
                throw new TrelloRequestFailedException(response.StatusCode, response.ReasonPhrase);
            }

            return response.Content.ReadAsStringAsync().Result;
        }

        private void IncludeKeyParameter(ref IDictionary<string, object> parameters)
        {
            if (parameters == null)
            {
                parameters = new Dictionary<string, object>();
            }

            if (!parameters.ContainsKey("key"))
            {
                parameters.Add("key", "key");
            }
        }

        private string FormatRequestUri(string resource, IEnumerable<KeyValuePair<string, object>> parameters)
        {
            var formattedParameters = new List<string>();

            foreach (var parameter in parameters)
            {
                formattedParameters.Add(string.Format("{0}={1}", parameter.Key, parameter.Value));
            }

            return string.Format("{0}?{1}", resource, string.Join("&", formattedParameters));
        }
    }
}
