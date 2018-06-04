using System.Reflection;
using NADS.Reflection.Data;

namespace NADS.Reflection.Generation
{
    public interface IPropertyDocGenerator : IMemberDocGenerator<PropertyInfo>
    {
        PropertyDoc GeneratePropertyDoc(PropertyInfo propertyInfo);
        
        (bool hasGet, bool hasSet) GenerateAccessorDoc(PropertyInfo propertyInfo);
    }
}