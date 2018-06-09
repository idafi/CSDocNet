using System.Collections.Generic;
using System.Reflection;
using NADS.Reflection.Data;

namespace NADS.Reflection.Generation
{
    public interface IConstructorDocGenerator : IMemberDocGenerator<ConstructorInfo>
    {
        MethodDoc GenerateConstructorDoc(ConstructorInfo ctorInfo);
    }
}