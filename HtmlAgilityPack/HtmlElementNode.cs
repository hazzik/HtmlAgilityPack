using System;

namespace HtmlAgilityPack
{
    using System.IO;

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
                if (name.Length > 0)
                {
                    if (name[0] == '?')
                        // forget this one, it's been done at the document level
                        return;

                    if (name.Trim().Length == 0)
                        return;
                    name = HtmlDocument.GetXmlName(name);
                }
                else
                    return;
            }
            outText.Write("<" + name);
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

                outText.Write("</" + name);
                if (!_ownerdocument.OptionOutputAsXml)
                    WriteAttributes(outText, true);

                outText.Write(">");
            }
            else
            {
                if (IsEmptyElement(Name))
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
                    outText.Write("></" + name + ">");
            }
            return;
        }
    }
}