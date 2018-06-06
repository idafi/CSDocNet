using System.Collections.Generic;
using NADS.Collections;

namespace NADS.Reflection.Data
{
    public readonly struct MemberDoc
    {
        public readonly string Name;
        public readonly string CommentID;

        public readonly AccessModifier Access;
        public readonly Modifier Modifiers;
        
        public readonly IReadOnlyList<MemberRef> Attributes;

        public MemberDoc(string name, string commentID, AccessModifier access, Modifier modifiers,
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