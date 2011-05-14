namespace HtmlAgilityPack
{
    internal abstract class HtmlElementNodeBase : HtmlNode
    {
        protected HtmlElementNodeBase(HtmlNodeType type, HtmlDocument ownerdocument, int index)
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
    }
}
