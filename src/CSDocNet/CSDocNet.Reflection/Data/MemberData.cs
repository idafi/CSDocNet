using System.Collections.Generic;
using CSDocNet.Collections;

namespace CSDocNet.Reflection.Data
{
    public class MemberData
    {
        public readonly string Name;
        public readonly string CommentID;

        public readonly AccessModifier Access;
        public readonly Modifier Modifiers;
        
        public readonly IReadOnlyList<MemberRef> Attributes;

        public MemberData(string name, string commentID, AccessModifier access, Modifier modifiers,
            IReadOnlyList<MemberRef> attributes)
        {
            Name = name ?? "";
            CommentID = commentID ?? "";

            Access = access;
            Modifiers = modifiers;
            
            Attributes = attributes ?? Empty<MemberRef>.List;
        }
    }
}