using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using Trellendar.Core.Extensions;
using Trellendar.DataAccess.Exceptions;

namespace Trellendar.DataAccess._Core._Impl
{
    public class RestClient : IRestClient
    {
        protected readonly HttpClient HttpClient;

        public RestClient(string baseAddress)
        {
            HttpClient = new HttpClient { BaseAddress = new Uri(baseAddress) };
        }

        public string FormatRequestUri(string resource, IDictionary<string, object> parameters)
        {
            if (parameters.IsNullOrEmpty())
            {
                return resource;
            }

            var formattedParameters = new List<string>();

            foreach (var parameter in parameters)
            {
                formattedParameters.Add("{0}={1}".FormatWith(parameter.Key, parameter.Value));
            }

            return "{0}?{1}".FormatWith(resource, formattedParameters.JoinWith("&"));
        }

        public string Get(string resource, IDictionary<string, object> parameters = null)
        {
            var requestUri = FormatRequestUri(resource, parameters);
            var response = HttpClient.GetAsync(requestUri).Result;

            return GetResponseContent(response);
        }

        public string Post(string resource, string jsonContent)
        {
            var messageContent = new StringContent(jsonContent);
            messageContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = HttpClient.PostAsync(resource, messageContent).Result;

            return GetResponseContent(response);
        }

        public string Post(string resource, IDictionary<string, object> parameters = null)
        {
            var content = parameters != null
                              ? new FormUrlEncodedContent(parameters.ToDictionary(x => x.Key, x => x.Value.ToString()))
                              : null;

            var response = HttpClient.PostAsync(resource, content).Result;

            return GetResponseContent(response);
        }

        private string GetResponseContent(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                throw new RequestFailedException(response.StatusCode, response.ReasonPhrase);
            }

            return response.Content.ReadAsStringAsync().Result;
        }
    }
}