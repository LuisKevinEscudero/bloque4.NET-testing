using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace _2.NetworkUtility.Ping
{
    public class NetworkService
    {
        public string SendPing()
        {
            return "Success: Ping Sent";
        }

        public int PingTimeout(int a, int b)
        {
            return a + b;
        }
        
        public DateTime LastPingDate()
        {
            return DateTime.Now;
        }

        public PingOptions PingOptions()
        {
            return new PingOptions()
            {
                DontFragment = true,
                Ttl=1
            };

        }
    }
}
