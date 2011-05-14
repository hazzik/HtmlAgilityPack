// HtmlAgilityPack V1.0 - Simon Mourier <simon underscore mourier at hotmail dot com>

namespace HtmlAgilityPack
{
    /// <summary>
    /// Represents an HTML comment.
    /// </summary>
    public class HtmlCommentNode : HtmlNode
    {
        private string _comment;

        internal HtmlCommentNode(HtmlDocument ownerdocument, int index)
            : base(HtmlNodeType.Comment, ownerdocument, index)
        {
        }

        /// <summary>
        /// Gets or Sets the comment text of the node.
        /// </summary>
        public string Comment
        {
            get { return _comment ?? base.InnerHtml; }
            set { _comment = value; }
        }

        /// <summary>
        /// Gets or Sets the HTML between the start and end tags of the object. In the case of a text node, it is equals to OuterHtml.
        /// </summary>
        public override string InnerHtml
        {
            get { return _comment ?? base.InnerHtml; }
            set { _comment = value; }
        }

        /// <summary>
        /// Gets or Sets the object and its content in HTML.
        /// </summary>
        public override string OuterHtml
        {
            get
            {
                return _comment == null
                           ? base.OuterHtml
                           : string.Format("<!--{0}-->", _comment);
            }
        }

        /// <summary>
        /// Gets or Sets the text between the start and end tags of the object.
        /// </summary>
        public override string InnerText
        {
            get { return Comment; }
        }
    }
}
