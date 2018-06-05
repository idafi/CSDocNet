using System.Reflection;
using NADS.Reflection.Data;

namespace NADS.Reflection.Generation
{
    public interface IPropertyDocGenerator : IMemberDocGenerator<PropertyInfo>
    {
        PropertyDoc GeneratePropertyDoc(PropertyInfo propertyInfo);
        
        PropertyDoc.Accessor GenerateAccessor(MethodInfo accessorInfo);
    }
}