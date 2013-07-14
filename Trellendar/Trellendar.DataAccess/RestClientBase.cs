using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using Trellendar.Core.Extensions;

namespace Trellendar.DataAccess
{
    public class RestClientBase : IRestClient
    {
        protected readonly HttpClient HttpClient;

        public RestClientBase(string baseAddress)
        {
            HttpClient = new HttpClient { BaseAddress = new Uri(baseAddress) };
        }

        public string FormatRequestUri(string resource, IDictionary<string, object> parameters, bool includeBaseAddress = false)
        {
            var formattedParameters = new List<string>();

            foreach (var parameter in parameters ?? new Dictionary<string, object>())
            {
                formattedParameters.Add("{0}={1}".FormatWith(parameter.Key, parameter.Value));
            }

            var formattedRequest = formattedParameters.Any()
                                       ? "{0}?{1}".FormatWith(resource, formattedParameters.JoinWith("&"))
                                       : resource;

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
            if (parameters == null)
            {
                parameters = new Dictionary<string, object>();
            }

            PreprocessRequest(resource, parameters);

            var requestUri = FormatRequestUri(resource, parameters);
            var response = HttpClient.GetAsync(requestUri).Result;

            return GetResponseContent(response);
        }

        public string Post(string resource, string jsonContent)
        {
            PreprocessRequest(resource, null);

            var messageContent = new StringContent(jsonContent);
            messageContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = HttpClient.PostAsync(resource, messageContent).Result;

            return GetResponseContent(response);
        }

        public string Post(string resource, IDictionary<string, object> parameters = null)
        {
            if (parameters == null)
            {
                parameters = new Dictionary<string, object>();
            }

            PreprocessRequest(resource, parameters);

            var content = parameters.IsNotNullOrEmpty()
                              ? new FormUrlEncodedContent(parameters.ToDictionary(x => x.Key, x => x.Value.ToString()))
                              : null;

            var response = HttpClient.PostAsync(resource, content).Result;

            return GetResponseContent(response);
        }

        public string Put(string resource, string jsonContent)
        {
            PreprocessRequest(resource, null);

            var messageContent = new StringContent(jsonContent);
            messageContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = HttpClient.PutAsync(resource, messageContent).Result;

            return GetResponseContent(response);
        }

        public string Delete(string resource, IDictionary<string, object> parameters = null)
        {
            if (parameters == null)
            {
                parameters = new Dictionary<string, object>();
            }

            PreprocessRequest(resource, parameters);

            var requestUri = FormatRequestUri(resource, parameters);
            var response = HttpClient.DeleteAsync(requestUri).Result;

            return GetResponseContent(response);
        }

        protected virtual void PreprocessRequest(string resource, IDictionary<string, object> parameters)
        {
        }

        private string GetResponseContent(HttpResponseMessage response)
        {
            response.EnsureSuccessStatusCode();

            return response.Content.ReadAsStringAsync().Result;
        }
    }
}