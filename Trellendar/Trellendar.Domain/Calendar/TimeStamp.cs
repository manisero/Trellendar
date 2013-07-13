using System;

namespace Trellendar.Domain.Calendar
{
    public class TimeStamp
    {
        public DateTime dateTime { get; set; }

        public TimeStamp()
        {
            
        }

        public TimeStamp(DateTime dateTime)
        {
            this.dateTime = dateTime;
        }
    }
}
