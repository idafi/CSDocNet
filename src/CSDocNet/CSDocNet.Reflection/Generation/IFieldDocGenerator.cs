using System.Reflection;
using CSDocNet.Reflection.Data;

namespace CSDocNet.Reflection.Generation
{
    public interface IFieldDataGenerator : IMemberDataGenerator<FieldInfo>
    {
        FieldData GenerateFieldData(FieldInfo fieldInfo);

        object GenerateConstValue(FieldInfo fieldInfo);
    }
}