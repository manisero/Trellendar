using System;
using System.Collections.Generic;
using System.Net.Http;
using Trellendar.Core.Extensions;
using Trellendar.DataAccess.Exceptions;

namespace Trellendar.DataAccess.Trello._Impl
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
                parameters.Add("key", TrelloKeys.APPLICATION_KEY);
            }

            if (!parameters.ContainsKey("token"))
            {
                parameters.Add("token", TrelloKeys.TOKEN);
            }
        }

        private string FormatRequestUri(string resource, IEnumerable<KeyValuePair<string, object>> parameters)
        {
            var formattedParameters = new List<string>();

            foreach (var parameter in parameters)
            {
                formattedParameters.Add("{0}={1}".FormatWith(parameter.Key, parameter.Value));
            }

            return "{0}?{1}".FormatWith(resource, formattedParameters.JoinWith("&"));
        }
    }
}
