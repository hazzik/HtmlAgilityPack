using System;

namespace HtmlAgilityPack
{
    internal class HtmlElementNode : HtmlNode
    {
        public HtmlElementNode(HtmlNodeType type, HtmlDocument ownerdocument, int index)
            : base(type, ownerdocument, index)
        {
        }

        /// <summary>
        /// Gets or Sets the text between the start and end tags of the object.
        /// </summary>
        public override string InnerText
        {
            get
            {
                // note: right now, this method is *slow*, because we recompute everything.
                // it could be optimised like innerhtml
                if (!HasChildNodes)
                {
                    return string.Empty;
                }

                string s = null;
                foreach (HtmlNode node in ChildNodes)
                {
                    s += node.InnerText;
                }
                return s;
            }
        }
    }
}
