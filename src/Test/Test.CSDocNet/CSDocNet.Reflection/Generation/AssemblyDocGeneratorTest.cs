using System.Reflection;
using NUnit.Framework;
using CSDocNet.Reflection.Data;
using CSDocNet.TestDoc;

namespace CSDocNet.Reflection.Generation
{
    [TestFixture]
    public class AssemblyDataGeneratorTest
    {
        AssemblyDataGenerator gen;

        [SetUp]
        public void SetUp()
        {
            gen = new AssemblyDataGenerator();
        }

        [Test]
        public void TestGenerateAssemblyData()
        {
            Assembly ass = Assembly.GetAssembly(typeof(TestClass));
            AssemblyData doc = gen.GenerateAssemblyData(ass);
        }
    }
}