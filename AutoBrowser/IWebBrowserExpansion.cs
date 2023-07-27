using CefSharp;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AutoBrowser
{
    public static class IWebBrowserExpansion
    {
        public static async Task<KeyValuePair<double, double>> GetKeyValuePairAsync(this IWebBrowser webBrowser, string xpath)
        {
            var getMainFrame = webBrowser.GetFocusedFrame();

            var list = new List<string>();
            list.Add("(");
            list.Add("function()");
            list.Add("{");
            list.Add("var element = document.evaluate (\"" + xpath + "\", document, null, XPathResult.FIRST_ORDERED_NODE_TYPE, null);");
            list.Add("var left = element.singleNodeValue.getBoundingClientRect().left;");
            list.Add("var right = element.singleNodeValue.getBoundingClientRect().right;");
            list.Add("var x = (left+right)/2;");
            list.Add("var top = element.singleNodeValue.getBoundingClientRect().top;");
            list.Add("var bottom = element.singleNodeValue.getBoundingClientRect().bottom;");
            list.Add("var y = (top+bottom)/2;");
            list.Add("return x+\"|\"+y;");
            list.Add("}");
            list.Add(")");
            list.Add("();");
            string join = string.Join("", list);
            Task<JavascriptResponse> evaluateScriptAsync = getMainFrame.EvaluateScriptAsync(join, null);
            JavascriptResponse evaluateScript = await evaluateScriptAsync;
            string @string = evaluateScript.Result?.ToString();
            if (string.IsNullOrWhiteSpace(@string)) throw new System.Exception(evaluateScript.Message);
            string[] split = @string.Split('|');
            string first = split.First();
            double key = double.Parse(first);
            string last = split.Last();
            double value = double.Parse(last);

            //await getMainFrame.EvaluateScriptAsync($"window.scrollTo(0,100)");

            var keyValuePair = new KeyValuePair<double, double>(key, value);
            return keyValuePair;
        }
        public static async Task SendKeyEventAsync(this IWebBrowser webBrowser, string xpath, string value)
        {
            await webBrowser.SendMouseClickEventAsync(xpath);
            IBrowser getBrowser = webBrowser.GetBrowser();
            IBrowserHost getHost = getBrowser.GetHost();
            Task run = Task.Run(() =>
            {
                foreach (char windowsKeyCode in value)
                {
                    var keyEvent = new KeyEvent
                    {
                        Type = KeyEventType.Char,
                        WindowsKeyCode = windowsKeyCode,
                    };
                    getHost.SendKeyEvent(keyEvent);
                    Thread.Sleep(10);
                }
            });
            await run;
        }
        public static async Task SendMouseClickEventAsync(this IWebBrowser webBrowser, string xpath)
        {
            IBrowser getBrowser = webBrowser.GetBrowser();
            IBrowserHost getHost = getBrowser.GetHost();
            var getKeyValuePair = await webBrowser.GetKeyValuePairAsync(xpath);
            Task run = Task.Run(() =>
            {
                getHost.SendMouseClickEvent((int)getKeyValuePair.Key, (int)getKeyValuePair.Value, MouseButtonType.Left, false, 1, CefEventFlags.None);
                Thread.Sleep(100);
                getHost.SendMouseClickEvent((int)getKeyValuePair.Key, (int)getKeyValuePair.Value, MouseButtonType.Left, true, 1, CefEventFlags.None);
            });
            await run;
        }
    }
}
