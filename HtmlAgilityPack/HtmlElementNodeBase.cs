namespace HtmlAgilityPack
{
    using System.IO;

    internal class HtmlElementNodeBase : HtmlNode
    {
        public HtmlElementNodeBase(HtmlNodeType type, HtmlDocument ownerdocument, int index)
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

        /// <summary>
        /// Creates a duplicate of the node.
        /// </summary>
        /// <param name="deep">true to recursively clone the subtree under the specified node; false to clone only the node itself.</param>
        /// <returns>The cloned node.</returns>
        public override HtmlNode CloneNode(bool deep)
        {
            HtmlNode node = _ownerdocument.CreateNode(NodeType);
            node.Name = Name;

            // attributes
            if (HasAttributes)
            {
                foreach (HtmlAttribute att in _attributes)
                {
                    HtmlAttribute newatt = att.Clone();
                    node.Attributes.Append(newatt);
                }
            }

            // closing attributes
            if (HasClosingAttributes)
            {
                node._endnode = _endnode.CloneNode(false);
                foreach (HtmlAttribute att in _endnode._attributes)
                {
                    HtmlAttribute newatt = att.Clone();
                    node._endnode._attributes.Append(newatt);
                }
            }
            if (!deep)
            {
                return node;
            }

            if (!HasChildNodes)
            {
                return node;
            }

            // child nodes
            foreach (HtmlNode child in _childnodes)
            {
                HtmlNode newchild = child.Clone();
                node.AppendChild(newchild);
            }
            return node;
        }

        /// <summary>
        /// Saves the current node to the specified TextWriter.
        /// </summary>
        /// <param name="outText">The TextWriter to which you want to save.</param>
        public override void WriteTo(TextWriter outText)
        {
            switch (_nodetype)
            {
                case HtmlNodeType.Document:
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
                                    break;
                                }
                            }
                        }
                    }
                    WriteContentTo(outText);
                    break;

                case HtmlNodeType.Element:
                    string name = _ownerdocument.OptionOutputUpperCase ? Name.ToUpper() : Name;

                    if (_ownerdocument.OptionOutputOriginalCase)
                        name = OriginalName;

                    if (_ownerdocument.OptionOutputAsXml)
                    {
                        if (name.Length > 0)
                        {
                            if (name[0] == '?')
                                // forget this one, it's been done at the document level
                                break;

                            if (name.Trim().Length == 0)
                                break;
                            name = HtmlDocument.GetXmlName(name);
                        }
                        else
                            break;
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
                    break;
            }
        }
    }
}