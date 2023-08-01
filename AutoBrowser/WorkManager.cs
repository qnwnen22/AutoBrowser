using System;
using System.Collections.Generic;

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

        public static string ToJson()
        {
            if (WorkEvents.Count <= 0)
                return null;

            return WorkEvents.ToJson();
        }
    }
}
