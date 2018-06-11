using System.Reflection;
using NADS.Reflection.Data;

namespace NADS.Reflection.Generation
{
    public interface IOperatorDataGenerator : IMemberDataGenerator<MethodInfo>
    {
        OperatorData GenerateOperatorData(MethodInfo opInfo);

        Operator GenerateOperator(MethodInfo opInfo);
    }
}