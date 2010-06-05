using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq.Expressions;
using System.Text;

namespace HtmlAgilityPack
{
    public partial class HtmlNode : DynamicObject
    {
        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            return base.TryInvokeMember(binder, args, out result);
        }
        public override bool TrySetMember(SetMemberBinder binder, object value)
        {

            return base.TrySetMember(binder, value);
        }
        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            if (binder.Name.StartsWith("_"))
                result = Attributes[binder.Name.Substring(1)];
            else
                result = ChildNodes[binder.Name];

            return result != null;
        }
    }

}
