using System.IO;
using System.Text;
using System.Xml;
using NUnit.Framework;

namespace NADS.Comments
{
    [TestFixture]
    public class AssemblyCommentParserTest
    {
        const string doc = @"<doc><assembly><name>Hello</name></assembly></doc>";
        const string badDoc = @"<doc<assembly><name>Hello</name></assembly></doc>";

        AssemblyCommentParser parser;

        [SetUp]
        public void SetUp()
        {
            parser = new AssemblyCommentParser();
        }

        [Test]
        public void TestParseFromStream()
        {
            byte[] asBytes = Encoding.UTF8.GetBytes(doc);
            MemoryStream mem = new MemoryStream(asBytes);

            var comments = parser.Parse(mem);
            Assert.AreEqual("Hello", comments.Name);
        }

        [Test]
        public void TestParseFromStreamCatchIfNull()
        {
            var comments = AssemblyComments.Empty;
            Assert.DoesNotThrow(() => comments = parser.Parse((Stream)(null)));
            Assert.AreEqual("", comments.Name);
        }

        [Test]
        public void TestParseFromStreamCatchIfMalformed()
        {
            byte[] asBytes = Encoding.UTF8.GetBytes(badDoc);
            MemoryStream mem = new MemoryStream(asBytes);

            var comments = AssemblyComments.Empty;
            Assert.DoesNotThrow(() => comments = parser.Parse(mem));
            Assert.AreEqual("", comments.Name);
        }
        
        [Test]
        public void TestParseFromString()
        {
            var comments = parser.Parse(doc);
            Assert.AreEqual("Hello", comments.Name);
        }

        [Test]
        public void TestParseFromStringCatchIfNull()
        {
            var comments = AssemblyComments.Empty;
            Assert.DoesNotThrow(() => comments = parser.Parse((string)(null)));
            Assert.AreEqual("", comments.Name);
        }

        [Test]
        public void TestParseFromStringCatchIfMalformed()
        {
            var comments = AssemblyComments.Empty;
            Assert.DoesNotThrow(() => comments = parser.Parse(badDoc));
            Assert.AreEqual("", comments.Name);
        }

        [Test]
        public void TestParseFromDocument()
        {
            var d = MakeDocument(doc);
            var comments = parser.Parse(d);
            Assert.AreEqual("Hello", comments.Name);
        }

        [Test]
        public void TestParseFromDocumentCatchIfNull()
        {
            var comments = AssemblyComments.Empty;
            Assert.DoesNotThrow(() => comments = parser.Parse((XmlDocument)(null)));
            Assert.AreEqual("", comments.Name);
        }

        [Test]
        public void TestParseName()
        {
            var assemblyNode = MakeElement(@"<assembly><name>Hello</name></assembly>");
            Assert.AreEqual("Hello", parser.ParseAssemblyName(assemblyNode));
        }

        [Test]
        public void TestParseNameNoNameElement()
        {
            var assemblyNode = MakeElement(@"<assembly>Hello</assembly>");
            Assert.AreEqual("", parser.ParseAssemblyName(assemblyNode));
        }

        [Test]
        public void TestParseNameFromDoc()
        {
            var d = MakeDocument(doc);
            var comments = parser.Parse(d);
            
            Assert.AreEqual("Hello", comments.Name);
        }

        XmlElement MakeElement(string xml)
        {
            var doc = MakeDocument(xml);
            return doc.DocumentElement;
        }

        XmlDocument MakeDocument(string xml)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);

            return doc;
        }
    }
}