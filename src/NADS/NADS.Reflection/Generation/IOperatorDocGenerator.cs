using System.Reflection;
using NADS.Reflection.Data;

namespace NADS.Reflection.Generation
{
    public interface IOperatorDocGenerator : IMemberDocGenerator<MethodInfo>
    {
        OperatorDoc GenerateOperatorDoc(MethodInfo opInfo);

        Operator GenerateOperator(MethodInfo opInfo);
    }
}