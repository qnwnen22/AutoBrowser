using CefSharp;
using System;
using System.Windows.Forms;

namespace AutoBrowser
{
    public partial class MainForm
    {
        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.chromiumWebBrowser.Back();
        }
        private void buttonForward_Click(object sender, EventArgs e)
        {
            this.chromiumWebBrowser.Forward();
        }
        private void buttonReload_Click(object sender, EventArgs e)
        {
            this.chromiumWebBrowser.Reload();
        }
        
    }
}
