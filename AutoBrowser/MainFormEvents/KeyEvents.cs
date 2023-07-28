using System;
using System.Windows.Forms;

namespace AutoBrowser
{
    public partial class MainForm
    {
        private void textBoxUrl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string address = null;
                try
                {
                    Uri uri = new Uri(this.textBoxUrl.Text);
                    address = this.textBoxUrl.Text;
                }
                catch
                {
                    address = $"https://www.google.com/search?q={this.textBoxUrl.Text}";
                }
                this.chromiumWebBrowser.Load(address);
            }
        }
    }
}
