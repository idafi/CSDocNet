using System.Collections.Generic;
using System.Reflection;
using NADS.Reflection.Data;

namespace NADS.Reflection.Generation
{
    public interface IMethodDataGenerator : IMemberDataGenerator<MethodInfo>
    {
        MethodData GenerateMethodData(MethodInfo methodInfo);

        ReturnValue GenerateReturnValue(MethodInfo methodInfo);
    }
}