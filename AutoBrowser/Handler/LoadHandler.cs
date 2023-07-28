using CefSharp;
using System;

namespace AutoBrowser.Handler
{
    public class LoadHandler : ILoadHandler
    {
        public virtual void OnFrameLoadEnd(IWebBrowser chromiumWebBrowser, FrameLoadEndEventArgs frameLoadEndArgs) { }

        public virtual void OnFrameLoadStart(IWebBrowser chromiumWebBrowser, FrameLoadStartEventArgs frameLoadStartArgs) { }

        public virtual void OnLoadError(IWebBrowser chromiumWebBrowser, LoadErrorEventArgs loadErrorArgs) { }

        public virtual void OnLoadingStateChange(IWebBrowser chromiumWebBrowser, LoadingStateChangedEventArgs loadingStateChangedArgs) { }
    }
}
