using System;

namespace Trellendar.Domain.Calendar
{
    public class TimeStamp
    {
        public DateTime? date { get; set; }

        public DateTime? dateTime { get; set; }

        public TimeStamp()
        {
            
        }

        public TimeStamp(DateTime timeStamp)
        {
            if (timeStamp.Date == timeStamp)
            {
                date = timeStamp;
            }
            else
            {
                dateTime = timeStamp;
            }
        }
    }
}
