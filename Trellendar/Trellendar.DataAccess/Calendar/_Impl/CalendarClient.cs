using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using Trellendar.Core.Extensions;
using Trellendar.DataAccess.Exceptions;
using System.Linq;

namespace Trellendar.DataAccess.Calendar._Impl
{
    public class CalendarClient : ICalendarClient
    {
        private readonly HttpClient _httpClient;

        public CalendarClient()
        {
            _httpClient = new HttpClient { BaseAddress = new Uri("https://www.googleapis.com/calendar/v3/") };
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", CalendarKeys.ACCESS_TOKEN);
        }

        public string FormatRequestUri(string resource, IEnumerable<KeyValuePair<string, object>> parameters)
        {
            if (parameters == null)
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
            if (resource == null)
            {
                throw new ArgumentNullException("resource");
            }

            var requestUri = FormatRequestUri(resource, parameters);
            var response = _httpClient.GetAsync(requestUri).Result;

            if (!response.IsSuccessStatusCode)
            {
                throw new RequestFailedException(response.StatusCode, response.ReasonPhrase);
            }

            return response.Content.ReadAsStringAsync().Result;
        }

        public string Post(string resource, string content)
        {
            if (resource == null)
            {
                throw new ArgumentNullException("resource");
            }

            var messageContent = new StringContent(content);
            messageContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = _httpClient.PostAsync(resource, messageContent).Result;

            if (!response.IsSuccessStatusCode)
            {
                throw new RequestFailedException(response.StatusCode, response.ReasonPhrase);
            }

            return response.Content.ReadAsStringAsync().Result;
        }

        public string Post(string resource, IDictionary<string, object> parameters = null)
        {
            if (resource == null)
            {
                throw new ArgumentNullException("resource");
            }

            var content = parameters != null
                              ? new FormUrlEncodedContent(parameters.ToDictionary(x => x.Key, x => x.Value.ToString()))
                              : null;

            var response = _httpClient.PostAsync(resource, content).Result;

            if (!response.IsSuccessStatusCode)
            {
                throw new RequestFailedException(response.StatusCode, response.ReasonPhrase);
            }

            return response.Content.ReadAsStringAsync().Result;
        }
    }
}