using System;
using System.Collections.Generic;
using System.Text;

namespace zero.Logging.Batching
{
    public class LogMessage
    {
        public DateTimeOffset Timestamp { get; set; }

        public string Message { get; set; }
    }
}
