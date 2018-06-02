using System.Collections.Generic;
using NADS.Collections;

namespace NADS.Reflection
{
    public readonly struct MemberDoc
    {
        public readonly string Name;
        public readonly string CommentID;

        public readonly AccessModifier Access;
        public readonly Modifier Modifiers;
        
        public readonly IReadOnlyList<MemberRef> Attributes;
        public readonly IReadOnlyList<TypeParam> TypeParams;

        public MemberDoc(string name, string commentID, AccessModifier access, Modifier modifiers,
            IReadOnlyList<MemberRef> attributes, IReadOnlyList<TypeParam> typeParams)
        {
            Name = name ?? "";
            CommentID = commentID ?? "";

            Access = access;
            Modifiers = modifiers;
            
            Attributes = attributes ?? Empty<MemberRef>.EmptyList;
            TypeParams = typeParams ?? Empty<TypeParam>.EmptyList;
        }
    }
}