using CefSharp;
using CefSharp.WinForms;
using System;

namespace AutoBrowser.Handler
{
    public class LifeSpanHandler : ILifeSpanHandler
    {
        public virtual bool DoClose(IWebBrowser chromiumWebBrowser, IBrowser browser)
        {
            return false;
        }
        public virtual void OnAfterCreated(IWebBrowser chromiumWebBrowser, IBrowser browser) { }
        public virtual void OnBeforeClose(IWebBrowser chromiumWebBrowser, IBrowser browser) { }
        public virtual bool OnBeforePopup(IWebBrowser chromiumWebBrowser,
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
            if (popupFeatures.IsPopup == false)
            {
                newBrowser = null;
                chromiumWebBrowser.Load(targetUrl);
                return true;
            }
            else
            {
                newBrowser = new ChromiumWebBrowser(targetUrl)
                {
                    Width = (int)popupFeatures.Width,
                    Height = (int)popupFeatures.Height,
                    LifeSpanHandler = new LifeSpanHandler(),
                };
                return false;
            }
        }
    }
}
