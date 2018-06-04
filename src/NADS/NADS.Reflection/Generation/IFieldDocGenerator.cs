using System.Reflection;
using NADS.Reflection.Data;

namespace NADS.Reflection.Generation
{
    public interface IFieldDocGenerator : IMemberDocGenerator<FieldInfo>
    {
        FieldDoc GenerateFieldDoc(FieldInfo fieldInfo);

        object GetConstValue(FieldInfo fieldInfo);
    }
}