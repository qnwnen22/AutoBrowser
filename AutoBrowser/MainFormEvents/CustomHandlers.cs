using AutoBrowser.Handler;
using CefSharp;
using System;

namespace AutoBrowser
{
    public partial class MainForm
    {
        private class MainLoadHandler : LoadHandler
        {
            MainForm mainForm;
            public LoadEndEvent LoadEndEvent { get; set; }

            public MainLoadHandler(MainForm form)
            {
                this.mainForm = form;
            }

            public override void OnFrameLoadEnd(IWebBrowser chromiumWebBrowser, FrameLoadEndEventArgs frameLoadEndArgs)
            {
                mainForm.textBoxUrl.Invoke(new Action(() =>
                {
                    mainForm.textBoxUrl.Text = chromiumWebBrowser.Address;
                    if (Properties.Settings.Default.IsSaveAddress)
                    {
                        Properties.Settings.Default.LastAddress = chromiumWebBrowser.Address;
                        Properties.Settings.Default.Save();
                    }
                }));
            }

        }
    }
}
