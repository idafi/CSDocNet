using System.Reflection;
using NADS.Reflection.Data;

namespace NADS.Reflection.Generation
{
    public interface IPropertyDataGenerator : IMemberDataGenerator<PropertyInfo>
    {
        PropertyData GeneratePropertyData(PropertyInfo propertyInfo);
        
        PropertyData.Accessor GenerateAccessor(MethodInfo accessorInfo);
    }
}