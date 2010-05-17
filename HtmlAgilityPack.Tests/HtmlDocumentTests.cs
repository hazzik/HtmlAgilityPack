using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace HtmlAgilityPack.Tests
{
    [TestFixture]
    public class HtmlDocumentTests
    {
        [Test]
        public void CreateAttribute()
        {
            HtmlDocument doc = new HtmlDocument();
            var a = doc.CreateAttribute("href");
            Assert.AreEqual("href", a.Name);
        }
        [Test]
        public void CreateAttributeWithText()
        {
            HtmlDocument doc = new HtmlDocument();
            var a = doc.CreateAttribute("href","http://something.com");
            Assert.AreEqual("href", a.Name);
            Assert.AreEqual("http://something.com", a.Value);
        }
        [Test]
        public void CreateAttributeWithEncodedText()
        {
            HtmlDocument doc = new HtmlDocument();
            var a = doc.CreateAttribute("href", "http://something.com\"&<>");
            Assert.AreEqual("href", a.Name);
            Assert.AreEqual("http://something.com\"&<>", a.Value);
            
        }
        [Test]
        public void HtmlEncode()
        {
            var result = HtmlDocument.HtmlEncode("http://something.com\"&<>");
            Assert.AreEqual("http://something.com&quot;&amp;&lt;&gt;", result);

        }
        [Test]
        public void CreateElement()
        {
            HtmlDocument doc = new HtmlDocument();
            var a = doc.CreateElement("a");
            Assert.AreEqual("a", a.Name);
            Assert.AreEqual(a.NodeType, HtmlNodeType.Element);
        }
        [Test]
        public void CreateComment()
        {
            HtmlDocument doc = new HtmlDocument();
            var a = doc.CreateComment();
            Assert.AreEqual(HtmlNode.HtmlNodeTypeNameComment, a.Name);
            Assert.AreEqual(a.NodeType, HtmlNodeType.Comment);
        }
        [Test]
        public void CreateCommentWithText()
        {
            HtmlDocument doc = new HtmlDocument();
            var a = doc.CreateComment("something");
            Assert.AreEqual(HtmlNode.HtmlNodeTypeNameComment, a.Name);
            Assert.AreEqual("something", a.InnerText);
            Assert.AreEqual(a.NodeType, HtmlNodeType.Comment);
        }
        [Test]
        public void CreateTextNode()
        {
            HtmlDocument doc = new HtmlDocument();
            var a = doc.CreateTextNode();
            Assert.AreEqual(HtmlNode.HtmlNodeTypeNameText, a.Name);
            Assert.AreEqual(a.NodeType, HtmlNodeType.Text);
        }
        [Test]
        public void CreateTextNodeWithText()
        {
            HtmlDocument doc = new HtmlDocument();
            var a = doc.CreateTextNode("something");
            Assert.AreEqual("something", a.InnerText);
            Assert.AreEqual(a.NodeType, HtmlNodeType.Text);
        }
    }
}
