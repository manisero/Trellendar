﻿using System.Collections.Generic;

namespace Trellendar.DataAccess
{
    public interface IRestClient
    {
        string FormatRequestUri(string resource, IDictionary<string, object> parameters, bool includeBaseAddress = false);

        string Get(string resource, IDictionary<string, object> parameters = null);

        string Post(string resource, string jsonContent);
        string Post(string resource, IDictionary<string, object> parameters = null);

        string Put(string resource, string jsonContent);

        string Delete(string resource, IDictionary<string, object> parameters = null);
    }
}