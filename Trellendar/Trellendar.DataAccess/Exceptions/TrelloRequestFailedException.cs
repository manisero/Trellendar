﻿using System;
using System.Net;

namespace Trellendar.DataAccess.Exceptions
{
    public class TrelloRequestFailedException : Exception
    {
        public HttpStatusCode Status { get; private set; }
        public string Reason { get; private set; }

        public TrelloRequestFailedException(HttpStatusCode status, string reason)
        {
            Status = status;
            Reason = reason;
        }
    }
}