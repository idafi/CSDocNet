using System.Collections.Generic;
using CSDocNet.Reflection.Data;

namespace CSDocNet.Reflection.Generation
{
    public interface IMemberDataGenerator<T>
    {
        MemberData GenerateMemberData(T member);
        
        string GenerateName(T member);
        string GenerateCommentID(T member);
        
        AccessModifier GenerateAccess(T member);
        Modifier GenerateModifiers(T member);
        
        IReadOnlyList<MemberRef> GenerateAttributes(T member);
    }
}