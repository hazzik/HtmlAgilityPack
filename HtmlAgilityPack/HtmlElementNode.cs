using System;

namespace HtmlAgilityPack
{
    using System.IO;
    using System.Xml;

    internal class HtmlElementNode : HtmlElementNodeBase
    {
        public HtmlElementNode(HtmlDocument ownerdocument, int index)
            : base(HtmlNodeType.Element, ownerdocument, index)
        {
        }

        /// <summary>
        /// Saves the current node to the specified TextWriter.
        /// </summary>
        /// <param name="outText">The TextWriter to which you want to save.</param>
        public override void WriteTo(TextWriter outText)
        {
            string name = _ownerdocument.OptionOutputUpperCase ? Name.ToUpper() : Name;
            if (_ownerdocument.OptionOutputOriginalCase)
                name = OriginalName;

            if (_ownerdocument.OptionOutputAsXml)
            {
                if (string.IsNullOrEmpty(name) || name.Trim() == string.Empty)
                    return;

                if (name[0] == '?')
                    // forget this one, it's been done at the document level
                    return;

                name = HtmlDocument.GetXmlName(name);
            }

            outText.Write("<{0}", name);
            WriteAttributes(outText, false);
            if (HasChildNodes)
            {
                outText.Write(">");
                bool cdata = false;
                if (_ownerdocument.OptionOutputAsXml && IsCDataElement(Name))
                {
                    // this code and the following tries to output things as nicely as possible for old browsers.
                    cdata = true;
                    outText.Write("\r\n//<![CDATA[\r\n");
                }


                if (cdata)
                {
                    if (HasChildNodes)
                        // child must be a text
                        ChildNodes[0].WriteTo(outText);

                    outText.Write("\r\n//]]>//\r\n");
                }
                else
                    WriteContentTo(outText);

                outText.Write("</{0}", name);
                if (!_ownerdocument.OptionOutputAsXml)
                    WriteAttributes(outText, true);

                outText.Write(">");
            }
            else if (IsEmptyElement(Name))
            {
                if ((_ownerdocument.OptionWriteEmptyNodes) || (_ownerdocument.OptionOutputAsXml))
                    outText.Write(" />");
                else
                {
                    if (Name.Length > 0 && Name[0] == '?')
                        outText.Write("?");

                    outText.Write(">");
                }
            }
            else
            {
                outText.Write("></{0}>", name);
            }
        }

        /// <summary>
        /// Saves the current node to the specified XmlWriter.
        /// </summary>
        /// <param name="writer">The XmlWriter to which you want to save.</param>
        public override void WriteTo(XmlWriter writer)
        {
            string name = _ownerdocument.OptionOutputUpperCase ? Name.ToUpper() : Name;

            if (_ownerdocument.OptionOutputOriginalCase)
                name = OriginalName;

            writer.WriteStartElement(name);
            WriteAttributes(writer, this);

            if (HasChildNodes)
            {
                foreach (HtmlNode subnode in ChildNodes)
                {
                    subnode.WriteTo(writer);
                }
            }
            writer.WriteEndElement();
        }
    }
}
