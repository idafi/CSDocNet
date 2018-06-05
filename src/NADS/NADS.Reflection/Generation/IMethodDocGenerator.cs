using System.Collections.Generic;
using System.Reflection;
using NADS.Reflection.Data;

namespace NADS.Reflection.Generation
{
    public interface IMethodDocGenerator : IMemberDocGenerator<MethodInfo>
    {
        MethodDoc GenerateMethodDoc(MethodInfo methodInfo);

        MemberRef GenerateReturnType(MethodInfo methodInfo);
        IReadOnlyList<Param> GenerateParams(MethodInfo methodInfo);
    }
}