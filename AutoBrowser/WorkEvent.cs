using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AutoBrowser
{
    public class WorkEvent
    {
        public BrowserEvent EventType { get; set; }
        public string Path { get; set; }
        public string Value { get; set; }

        public WorkEvent()
        {

        }
        
        public WorkEvent(BrowserEvent browserEvent)
        {
            EventType = browserEvent;
        }
        public WorkEvent(BrowserEvent browserEvent, string value) : this(browserEvent)
        {
            this.Value = value;
        }

        public WorkEvent(BrowserEvent browserEvent, string xPath, string value) : this(browserEvent, value)
        {
            this.Path = xPath;
        }
        public override string ToString()
        {
            string result = $"EventType={EventType}\nXPath={Path}\nValue={Value}";
            return result;
        }
    }
}
