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

        public string FormatRequestUri(string resource, IDictionary<string, object> parameters, bool includeBaseAddress = false)
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

            var formattedRequest = "{0}?{1}".FormatWith(resource, formattedParameters.JoinWith("&"));

            if (includeBaseAddress)
            {
                return (HttpClient.BaseAddress.AbsoluteUri.EndsWith("/") ? "{0}{1}" : "{0}/{1}")
                            .FormatWith(HttpClient.BaseAddress.AbsoluteUri, formattedRequest);
            }
            else
            {
                return formattedRequest;
            }
        }

        public string Get(string resource, IDictionary<string, object> parameters = null)
        {
            IncludeAuthorizationParameters(ref parameters);

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
            IncludeAuthorizationParameters(ref parameters);

            var content = parameters != null
                              ? new FormUrlEncodedContent(parameters.ToDictionary(x => x.Key, x => x.Value.ToString()))
                              : null;

            var response = HttpClient.PostAsync(resource, content).Result;

            return GetResponseContent(response);
        }

        protected virtual void IncludeAuthorizationParameters(ref IDictionary<string, object> parameters)
        {
        }

        private string GetResponseContent(HttpResponseMessage response)
        {
            response.EnsureSuccessStatusCode();

            return response.Content.ReadAsStringAsync().Result;
        }
    }
}