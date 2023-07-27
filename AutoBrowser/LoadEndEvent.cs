using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoBrowser
{
    public class LoadEndEvent
    {
        public string Address { get; set; }
        public List<WorkEvent> LoginEvnets { get; set; }
        public LoadEndEvent(string address, List<WorkEvent> evnets)
        {
            Address = address;
            LoginEvnets = evnets;
        }
    }
}
