using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace HtmlAgilityPack.Metro.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public async void TestHtmlWebBasicCall()
        {
            var html = new HtmlWeb();
            await html.LoadFromWebAsync("http://www.google.com");

        }
    }
}
