using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoBrowser
{
    public partial class MainForm
    {
        private async Task WaitAsync(int millisecondsTimeout)
        {
            await Task.Run((() =>
            {
                Thread.Sleep(millisecondsTimeout);
            }));
        }

        public void DataReload(List<WorkEvent> workEvents = null)
        {
            if (workEvents != null)
            {
                WorkManager.WorkEvents = workEvents;
                bindings = new BindingList<WorkEvent>(WorkManager.WorkEvents);
                this.dataGridViewEvents.DataSource = bindings;
            }
            else
            {
                bindings.ResetBindings();
            }
        }
    }
}
