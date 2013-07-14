﻿using System.Net.Http.Headers;
using Trellendar.DataAccess._Core._Impl;

namespace Trellendar.DataAccess.Calendar._Impl
{
    public class CalendarClient : RestClient, ICalendarClient
    {
        public CalendarClient(UserContext userContext) : base("https://www.googleapis.com/calendar/v3/")
        {
            if (userContext.IsFilled())
            {
                HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userContext.User.CalendarAccessToken);
            }
        }
    }
}