using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace UptimeMonitor
{
    internal class DiscordWebhookPayload
    {
        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "type")]
        public int Type { get; set; }

        [DataMember(Name = "channel_id")]
        public long ChannelId { get; set; }

        [DataMember(Name = "token")]
        public string Token { get; set; }

        [DataMember(Name = "id")]
        public long Id { get; set; }

        [DataMember(Name = "content")]
        public string Content { get; set; }
        /*
         * 
         * {
  "name": "test webhook",
  "type": 1,
  "channel_id": "1245545099707027478",
  "token": "6Cw7nYZ4D42JcfgMd7QJAYjzoTVVbK_bp6usZ13Gv21DOBswBjfZg3uJ1AnuekAjS-Td",
  "id": "223704706495545344",
  "content": "test"
}
        */
    }
}
