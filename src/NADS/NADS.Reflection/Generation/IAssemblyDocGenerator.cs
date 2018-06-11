using System.Reflection;
using NADS.Reflection.Data;

namespace NADS.Reflection.Generation
{
    public interface IAssemblyDocGenerator
    {
        AssemblyDoc GenerateAssemblyDoc(Assembly assembly);
    }
}