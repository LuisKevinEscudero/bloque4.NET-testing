using System;
using System.Collections.Generic;
using System.Linq;
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
        
    }
}
