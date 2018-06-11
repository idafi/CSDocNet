using System.Reflection;
using NADS.Reflection.Data;

namespace NADS.Reflection.Generation
{
    public interface IFieldDataGenerator : IMemberDataGenerator<FieldInfo>
    {
        FieldData GenerateFieldData(FieldInfo fieldInfo);

        object GenerateConstValue(FieldInfo fieldInfo);
    }
}