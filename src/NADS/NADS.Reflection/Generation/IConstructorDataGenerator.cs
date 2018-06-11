using System.Collections.Generic;
using System.Reflection;
using NADS.Reflection.Data;

namespace NADS.Reflection.Generation
{
    public interface IConstructorDataGenerator : IMemberDataGenerator<ConstructorInfo>
    {
        MethodData GenerateConstructorData(ConstructorInfo ctorInfo);
    }
}