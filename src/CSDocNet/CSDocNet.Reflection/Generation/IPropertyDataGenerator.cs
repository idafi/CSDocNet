using System.Reflection;
using CSDocNet.Reflection.Data;

namespace CSDocNet.Reflection.Generation
{
    public interface IPropertyDataGenerator : IMemberDataGenerator<PropertyInfo>
    {
        PropertyData GeneratePropertyData(PropertyInfo propertyInfo);
        
        PropertyData.Accessor GenerateAccessor(MethodInfo accessorInfo);
    }
}