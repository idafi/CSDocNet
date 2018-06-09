using System.Collections.Generic;
using System.Reflection;
using NADS.Reflection.Data;

namespace NADS.Reflection.Generation
{
    public interface IMethodBaseUtility
    {
        MemberDoc GenerateMemberDoc(MethodBase member);

        string GenerateName(MethodBase member);
        string GenerateCommentID(MethodBase member);
        AccessModifier GenerateAccess(MethodBase member);
        Modifier GenerateModifiers(MethodBase member);
        IReadOnlyList<MemberRef> GenerateAttributes(MethodBase member);

        IReadOnlyList<Param> GenerateParams(MethodBase methodInfo);
        IReadOnlyList<TypeParam> GenerateTypeParams(MethodBase methodInfo);
    }
}