using System.Collections.Generic;
using System.Reflection;
using CSDocNet.Reflection.Data;

namespace CSDocNet.Reflection.Generation
{
    public interface IMethodBaseUtility
    {
        MemberData GenerateMemberData(MethodBase member);

        string GenerateName(MethodBase member);
        string GenerateCommentID(MethodBase member);
        AccessModifier GenerateAccess(MethodBase member);
        Modifier GenerateModifiers(MethodBase member);
        IReadOnlyList<MemberRef> GenerateAttributes(MethodBase member);

        IReadOnlyList<Param> GenerateParams(MethodBase methodInfo);
        IReadOnlyList<TypeParam> GenerateTypeParams(MethodBase methodInfo);
    }
}