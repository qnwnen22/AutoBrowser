using CefSharp;
using System;

namespace AutoBrowser
{
    public partial class MainForm
    {
        public class LoadHandler : ILoadHandler
        {
            MainForm mainForm;
            public LoadEndEvent LoadEndEvent { get; set; }

            public LoadHandler(MainForm form)
            {
                this.mainForm = form;
            }

            public void OnFrameLoadEnd(IWebBrowser chromiumWebBrowser, FrameLoadEndEventArgs frameLoadEndArgs)
            {
                mainForm.textBoxUrl.Invoke(new Action(() =>
                {
                    mainForm.textBoxUrl.Text = chromiumWebBrowser.Address;
                    Properties.Settings.Default.LastAddress = chromiumWebBrowser.Address;
                    Properties.Settings.Default.Save();
                }));
            }

            public void OnFrameLoadStart(IWebBrowser chromiumWebBrowser, FrameLoadStartEventArgs frameLoadStartArgs)
            {
            }

            public void OnLoadError(IWebBrowser chromiumWebBrowser, LoadErrorEventArgs loadErrorArgs)
            {
            }

            public void OnLoadingStateChange(IWebBrowser chromiumWebBrowser, LoadingStateChangedEventArgs loadingStateChangedArgs)
            {
            }
        }


        public class LifeSpanHandler : ILifeSpanHandler
        {
            public bool DoClose(IWebBrowser chromiumWebBrowser, IBrowser browser)
            {
                return true;
            }

            public void OnAfterCreated(IWebBrowser chromiumWebBrowser, IBrowser browser) { }

            public void OnBeforeClose(IWebBrowser chromiumWebBrowser, IBrowser browser) { }

            public bool OnBeforePopup(IWebBrowser chromiumWebBrowser,
                                      IBrowser browser,
                                      IFrame frame,
                                      string targetUrl,
                                      string targetFrameName,
                                      WindowOpenDisposition targetDisposition,
                                      bool userGesture,
                                      IPopupFeatures popupFeatures,
                                      IWindowInfo windowInfo,
                                      IBrowserSettings browserSettings,
                                      ref bool noJavascriptAccess,
                                      out IWebBrowser newBrowser)
            {
                newBrowser = null;
                if (popupFeatures.IsPopup == false)
                {
                    chromiumWebBrowser.Load(targetUrl);
                }
                return true;
            }
        }
    }
}
