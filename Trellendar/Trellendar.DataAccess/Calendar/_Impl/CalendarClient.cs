using System;
using System.Collections.Generic;
using System.Net.Http;
using Trellendar.Core.Extensions;
using Trellendar.DataAccess.Exceptions;

namespace Trellendar.DataAccess.Calendar._Impl
{
    public class CalendarClient : ICalendarClient
    {
        private readonly HttpClient _httpClient;

        public CalendarClient()
        {
            _httpClient = new HttpClient { BaseAddress = new Uri("https://www.googleapis.com/calendar/v3/") };
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