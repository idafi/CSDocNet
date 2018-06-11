using System.Collections.Generic;
using System.Reflection;
using CSDocNet.Reflection.Data;

namespace CSDocNet.Reflection.Generation
{
    public interface IMethodDataGenerator : IMemberDataGenerator<MethodInfo>
    {
        MethodData GenerateMethodData(MethodInfo methodInfo);

        ReturnValue GenerateReturnValue(MethodInfo methodInfo);
    }
}