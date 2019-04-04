using System.Collections.Generic;

namespace andead.netcore.mqtt.Models
{
    public class rule
    {
        public string device { get; set; }
        public string criteria { get; set; }
        public string condition { get; set; }
        public string value { get; set; }
    }

    public class Logic
    {
        public string op { get; set; }
        public rule @rule { get; set; }
        public Logic logic { get; set; }
    }

    public class Rule
    {
        public int id { get; set; }
        public string name { get; set; }
        public int user_id { get; set; }
        public Logic logic { get; set; }
    }
}