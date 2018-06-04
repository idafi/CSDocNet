using System.Collections.Generic;
using NADS.Reflection.Data;

namespace NADS.Reflection.Generation
{
    public interface IMemberDocGenerator<T>
    {
        MemberDoc GenerateMemberDoc(T member);
        
        string GenerateName(T member);
        string GenerateCommentID(T member);
        
        AccessModifier GenerateAccess(T member);
        Modifier GenerateModifiers(T member);
        
        IReadOnlyList<MemberRef> GenerateAttributes(T member);
    }
}