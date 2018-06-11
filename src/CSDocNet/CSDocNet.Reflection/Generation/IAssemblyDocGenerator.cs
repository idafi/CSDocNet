using System.Reflection;
using CSDocNet.Reflection.Data;

namespace CSDocNet.Reflection.Generation
{
    public interface IAssemblyDataGenerator
    {
        AssemblyData GenerateAssemblyData(Assembly assembly);
    }
}