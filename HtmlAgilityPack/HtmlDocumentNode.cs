namespace HtmlAgilityPack
{
    internal class HtmlDocumentNode : HtmlElementNodeBase
    {
        public HtmlDocumentNode(HtmlDocument ownerdocument, int index)
            : base(HtmlNodeType.Document, ownerdocument, index)
        {
        }
    }
}