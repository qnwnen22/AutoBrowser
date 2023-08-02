using CefSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoBrowser.Handler
{
    public class StringVisitor : IStringVisitor
    {
        string html;

        private static StringVisitor instance;
        public static StringVisitor Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new StringVisitor();
                }
                return instance;
            }
        }

        private StringVisitor() { }


        public void Dispose()
        {

        }

        public void Visit(string str)
        {
            html = str;
        }

        public string GetHtml()
        {
            return html;
        }
    }
}
