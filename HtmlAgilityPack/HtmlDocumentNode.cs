namespace HtmlAgilityPack
{
    using System.IO;

    internal class HtmlDocumentNode : HtmlElementNodeBase
    {
        public HtmlDocumentNode(HtmlDocument ownerdocument, int index)
            : base(HtmlNodeType.Document, ownerdocument, index)
        {
        }

        /// <summary>
        /// Saves the current node to the specified TextWriter.
        /// </summary>
        /// <param name="outText">The TextWriter to which you want to save.</param>
        public override void WriteTo(TextWriter outText)
        {
            if (_ownerdocument.OptionOutputAsXml)
            {
#if SILVERLIGHT || PocketPC
                outText.Write("<?xml version=\"1.0\" encoding=\"" + _ownerdocument.GetOutEncoding().WebName +
                                             "\"?>");
#else
                outText.Write("<?xml version=\"1.0\" encoding=\"" + _ownerdocument.GetOutEncoding().BodyName +
                                "\"?>");
#endif
                // check there is a root element
                if (_ownerdocument.DocumentNode.HasChildNodes)
                {
                    int rootnodes = _ownerdocument.DocumentNode._childnodes.Count;
                    if (rootnodes > 0)
                    {
                        HtmlNode xml = _ownerdocument.GetXmlDeclaration();
                        if (xml != null)
                            rootnodes --;

                        if (rootnodes > 1)
                        {
                            if (_ownerdocument.OptionOutputUpperCase)
                            {
                                outText.Write("<SPAN>");
                                WriteContentTo(outText);
                                outText.Write("</SPAN>");
                            }
                            else
                            {
                                outText.Write("<span>");
                                WriteContentTo(outText);
                                outText.Write("</span>");
                            }
                            return;
                        }
                    }
                }
            }
            WriteContentTo(outText);
            return;
        }
    }
}