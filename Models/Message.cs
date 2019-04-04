using System;
using System.Collections.Generic;

namespace andead.netcore.mqtt.Models
{
    public class Message
    {
        public Guid id { get; set; }

        public DateTime date_time { get; set; }

        public int user_id { get; set; }

        public object message { get; set; }
    }
}