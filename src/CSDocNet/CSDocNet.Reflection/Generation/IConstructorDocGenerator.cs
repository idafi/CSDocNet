using System.Collections.Generic;
using System.Reflection;
using CSDocNet.Reflection.Data;

namespace CSDocNet.Reflection.Generation
{
    public interface IConstructorDataGenerator : IMemberDataGenerator<ConstructorInfo>
    {
        MethodData GenerateConstructorData(ConstructorInfo ctorInfo);
    }
}