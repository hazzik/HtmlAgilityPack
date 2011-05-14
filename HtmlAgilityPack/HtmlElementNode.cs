using System;

namespace HtmlAgilityPack
{
    internal class HtmlElementNode : HtmlElementNodeBase
    {
        public HtmlElementNode(HtmlDocument ownerdocument, int index)
            : base(HtmlNodeType.Element, ownerdocument, index)
        {
        }
    }
}
