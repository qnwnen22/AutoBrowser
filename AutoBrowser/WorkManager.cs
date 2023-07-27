using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoBrowser
{
    public class WorkManager
    {
        private WorkManager() { }

        private static List<WorkEvent> workEvents;
        public static List<WorkEvent> WorkEvents
        {
            get
            {
                if (workEvents == null) workEvents = new List<WorkEvent>();
                return workEvents;
            }
            set
            {
                workEvents = value;
            }
        }
    }
}
