using System.Reflection;
using CSDocNet.Reflection.Data;

namespace CSDocNet.Reflection.Generation
{
    public interface IOperatorDataGenerator : IMemberDataGenerator<MethodInfo>
    {
        OperatorData GenerateOperatorData(MethodInfo opInfo);

        Operator GenerateOperator(MethodInfo opInfo);
    }
}