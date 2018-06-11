using System.Reflection;
using NADS.Reflection.Data;

namespace NADS.Reflection.Generation
{
    public interface IAssemblyDataGenerator
    {
        AssemblyData GenerateAssemblyData(Assembly assembly);
    }
}