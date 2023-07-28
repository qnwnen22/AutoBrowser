using System;
using System.Windows.Forms;

namespace AutoBrowser
{
    public partial class MainForm
    {
        void BrowserLoad(string url)
        {
            string address = null;
            try
            {
                Uri uri = new Uri(url);
                address = url;
            }
            catch
            {
                address = $"https://www.google.com/search?q={url}";
            }
            this.chromiumWebBrowser.Load(address);
        }

        private void textBoxUrl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BrowserLoad(this.textBoxUrl.Text);
            }
        }
    }
}
