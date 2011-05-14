using System;

namespace HtmlAgilityPack
{
    internal class HtmlElementNode : HtmlNode
    {
        public HtmlElementNode(HtmlNodeType type, HtmlDocument ownerdocument, int index)
            : base(type, ownerdocument, index)
        {
        }
    }
}
