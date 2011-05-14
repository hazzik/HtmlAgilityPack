// HtmlAgilityPack V1.0 - Simon Mourier <simon underscore mourier at hotmail dot com>

namespace HtmlAgilityPack
{
    /// <summary>
    /// Represents an HTML text node.
    /// </summary>
    public class HtmlTextNode : HtmlNode
    {
        private string _text;

        internal HtmlTextNode(HtmlDocument ownerdocument, int index)
            : base(HtmlNodeType.Text, ownerdocument, index)
        {
        }

        /// <summary>
        /// Gets or Sets the HTML between the start and end tags of the object. In the case of a text node, it is equals to OuterHtml.
        /// </summary>
        public override string InnerHtml
        {
            get { return OuterHtml; }
            set { _text = value; }
        }

        /// <summary>
        /// Gets or Sets the object and its content in HTML.
        /// </summary>
        public override string OuterHtml
        {
            get { return _text ?? base.OuterHtml; }
        }

        /// <summary>
        /// Gets or Sets the text of the node.
        /// </summary>
        public string Text
        {
            get { return _text ?? base.OuterHtml; }
            set { _text = value; }
        }

        /// <summary>
        /// Gets or Sets the text between the start and end tags of the object.
        /// </summary>
        public override string InnerText
        {
            get { return Text; }
        }
    }
}
