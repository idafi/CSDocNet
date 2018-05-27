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
        public void TestParseNameNoAssemblyElement()
        {
            string xml = @"<doc><name>Hello</name></doc>";
            var d = MakeDocument(xml);
            var comments = parser.Parse(d);

            Assert.AreEqual("", comments.Name);
        }

        [Test]
        public void TestParseNameWithNullNode()
        {
            Assert.AreEqual("", parser.ParseAssemblyName(null));
        }

        [Test]
        public void TestParseNameFromDoc()
        {
            var d = MakeDocument(doc);
            var comments = parser.Parse(d);
            
            Assert.AreEqual("Hello", comments.Name);
        }

        [Test]
        public void TestParseText()
        {
            string xml = @"<doc>hello i am text</doc>";
            var element = MakeElement(xml);
            var block = parser.ParseCommentBlock(element);

            Assert.AreEqual(CommentNodeType.Text, block.Nodes[0].Type);
            Assert.AreEqual("hello i am text", block.Text[0]);
        }

        [Test]
        public void TestParseTextWithEmptyBlock()
        {
            string xml = @"<doc></doc>";
            var element = MakeElement(xml);
            var block = parser.ParseCommentBlock(element);

            Assert.AreEqual(0, block.Nodes.Count);
            Assert.AreEqual(0, block.Text.Count);
        }

        [Test]
        public void TestParseCRef()
        {
            string xml = @"<doc><see cref=""wowzer""/></doc>";
            var element = MakeElement(xml);
            var block = parser.ParseCommentBlock(element);

            Assert.AreEqual(CommentNodeType.CRef, block.Nodes[0].Type);
            Assert.AreEqual("wowzer", block.Text[0]);
        }

        [Test]
        public void TestParseCRefWhenEmpty()
        {
            string xml = @"<doc><see cref=""""/></doc>";
            var element = MakeElement(xml);
            var block = parser.ParseCommentBlock(element);

            Assert.AreEqual(CommentNodeType.CRef, block.Nodes[0].Type);
            Assert.AreEqual("", block.Text[0]);
        }

        [Test]
        public void TestParseCRefWithBadAttribute()
        {
            string xml = @"<doc><see cfer=""wowzer""/></doc>";
            var element = MakeElement(xml);
            var block = parser.ParseCommentBlock(element);

            Assert.AreEqual(0, block.Nodes.Count);
            Assert.AreEqual(0, block.Text.Count);
        }

        [Test]
        public void TestParseCRefWithNoAttribute()
        {
            string xml = @"<doc><see/></doc>";
            var element = MakeElement(xml);
            var block = parser.ParseCommentBlock(element);

            Assert.AreEqual(0, block.Nodes.Count);
            Assert.AreEqual(0, block.Text.Count);
        }

        [Test]
        public void TestParseParamRef()
        {
            string xml = @"<doc><paramref name=""wowzer""/></doc>";
            var element = MakeElement(xml);
            var block = parser.ParseCommentBlock(element);

            Assert.AreEqual(CommentNodeType.ParamRef, block.Nodes[0].Type);
            Assert.AreEqual("wowzer", block.Text[0]);
        }

        [Test]
        public void TestParseParamRefWhenEmpty()
        {
            string xml = @"<doc><paramref name=""""/></doc>";
            var element = MakeElement(xml);
            var block = parser.ParseCommentBlock(element);

            Assert.AreEqual(CommentNodeType.ParamRef, block.Nodes[0].Type);
            Assert.AreEqual("", block.Text[0]);
        }

        [Test]
        public void TestParseParamRefWithBadAttribute()
        {
            string xml = @"<doc><paramref mane=""wowzer""/></doc>";
            var element = MakeElement(xml);
            var block = parser.ParseCommentBlock(element);

            Assert.AreEqual(0, block.Nodes.Count);
            Assert.AreEqual(0, block.Text.Count);
        }

        [Test]
        public void TestParseParamRefWithNoAttribute()
        {
            string xml = @"<doc><paramref/></doc>";
            var element = MakeElement(xml);
            var block = parser.ParseCommentBlock(element);

            Assert.AreEqual(0, block.Nodes.Count);
            Assert.AreEqual(0, block.Text.Count);
        }

        [Test]
        public void TestParseTypeParamRef()
        {
            string xml = @"<doc><typeparamref name=""wowzer""/></doc>";
            var element = MakeElement(xml);
            var block = parser.ParseCommentBlock(element);

            Assert.AreEqual(CommentNodeType.TypeParamRef, block.Nodes[0].Type);
            Assert.AreEqual("wowzer", block.Text[0]);
        }

        [Test]
        public void TestParseTypeParamRefWhenEmpty()
        {
            string xml = @"<doc><typeparamref name=""""/></doc>";
            var element = MakeElement(xml);
            var block = parser.ParseCommentBlock(element);

            Assert.AreEqual(CommentNodeType.TypeParamRef, block.Nodes[0].Type);
            Assert.AreEqual("", block.Text[0]);
        }

        [Test]
        public void TestParseTypeParamRefWithBadAttribute()
        {
            string xml = @"<doc><typeparamref mane=""wowzer""/></doc>";
            var element = MakeElement(xml);
            var block = parser.ParseCommentBlock(element);

            Assert.AreEqual(0, block.Nodes.Count);
            Assert.AreEqual(0, block.Text.Count);
        }

        [Test]
        public void TestParseTypeParamRefWithNoAttribute()
        {
            string xml = @"<doc><typeparamref/></doc>";
            var element = MakeElement(xml);
            var block = parser.ParseCommentBlock(element);

            Assert.AreEqual(0, block.Nodes.Count);
            Assert.AreEqual(0, block.Text.Count);
        }

        [Test]
        public void TestParseParagraph()
        {
            string xml = @"<doc><para>hello i'm a para</para></doc>";
            var element = MakeElement(xml);
            var block = parser.ParseCommentBlock(element);
            var paraBlock = block.Blocks[0];

            Assert.AreEqual(CommentNodeType.Paragraph, block.Nodes[0].Type);
            Assert.AreEqual(CommentNodeType.Text, paraBlock.Nodes[0].Type);
            Assert.AreEqual("hello i'm a para", paraBlock.Text[0]);
        }

        [Test]
        public void TestParseParagraphWhenEmpty()
        {
            string xml = @"<doc><para></para></doc>";
            var element = MakeElement(xml);
            var block = parser.ParseCommentBlock(element);
            var paraBlock = block.Blocks[0];

            Assert.AreEqual(CommentNodeType.Paragraph, block.Nodes[0].Type);
            Assert.AreEqual(0, paraBlock.Nodes.Count);
            Assert.AreEqual(0, paraBlock.Text.Count);
        }

        [Test]
        public void TestParseParagraphNested()
        {
            string xml = @"<doc><para>oh no it's <para>another para</para></para></doc>";
            var element = MakeElement(xml);
            var block = parser.ParseCommentBlock(element);
            var outerPara = block.Blocks[0];
            var innerPara = outerPara.Blocks[0];

            Assert.AreEqual(CommentNodeType.Paragraph, block.Nodes[0].Type);
            Assert.AreEqual(CommentNodeType.Text, outerPara.Nodes[0].Type);
            Assert.AreEqual(CommentNodeType.Paragraph, outerPara.Nodes[1].Type);
            Assert.AreEqual(CommentNodeType.Text, innerPara.Nodes[0].Type);
            Assert.AreEqual("oh no it's ", outerPara.Text[0]);
            Assert.AreEqual("another para", innerPara.Text[0]);
        }

        [Test]
        public void TestParseCodeBlock()
        {
            string xml = @"<doc><code>hello i'm a code</code></doc>";
            var element = MakeElement(xml);
            var block = parser.ParseCommentBlock(element);
            var codeBlock = block.Blocks[0];

            Assert.AreEqual(CommentNodeType.CodeBlock, block.Nodes[0].Type);
            Assert.AreEqual(CommentNodeType.Text, codeBlock.Nodes[0].Type);
            Assert.AreEqual("hello i'm a code", codeBlock.Text[0]);
        }

        [Test]
        public void TestParseCodeBlockWhenEmpty()
        {
            string xml = @"<doc><code></code></doc>";
            var element = MakeElement(xml);
            var block = parser.ParseCommentBlock(element);
            var codeBlock = block.Blocks[0];

            Assert.AreEqual(CommentNodeType.CodeBlock, block.Nodes[0].Type);
            Assert.AreEqual(0, codeBlock.Nodes.Count);
            Assert.AreEqual(0, codeBlock.Text.Count);
        }

        [Test]
        public void TestParseCodeBlockNested()
        {
            string xml = @"<doc><code>oh no it's <code>another code</code></code></doc>";
            var element = MakeElement(xml);
            var block = parser.ParseCommentBlock(element);
            var outerCode = block.Blocks[0];
            var innerCode = outerCode.Blocks[0];

            Assert.AreEqual(CommentNodeType.CodeBlock, block.Nodes[0].Type);
            Assert.AreEqual(CommentNodeType.Text, outerCode.Nodes[0].Type);
            Assert.AreEqual(CommentNodeType.CodeBlock, outerCode.Nodes[1].Type);
            Assert.AreEqual(CommentNodeType.Text, innerCode.Nodes[0].Type);
            Assert.AreEqual("oh no it's ", outerCode.Text[0]);
            Assert.AreEqual("another code", innerCode.Text[0]);
        }

        [Test]
        public void TestParseCodeInline()
        {
            string xml = @"<doc><c>hello i'm a code</c></doc>";
            var element = MakeElement(xml);
            var block = parser.ParseCommentBlock(element);
            var codeBlock = block.Blocks[0];

            Assert.AreEqual(CommentNodeType.CodeInline, block.Nodes[0].Type);
            Assert.AreEqual(CommentNodeType.Text, codeBlock.Nodes[0].Type);
            Assert.AreEqual("hello i'm a code", codeBlock.Text[0]);
        }

        [Test]
        public void TestParseCodeInlineWhenEmpty()
        {
            string xml = @"<doc><c></c></doc>";
            var element = MakeElement(xml);
            var block = parser.ParseCommentBlock(element);
            var codeBlock = block.Blocks[0];

            Assert.AreEqual(CommentNodeType.CodeInline, block.Nodes[0].Type);
            Assert.AreEqual(0, codeBlock.Nodes.Count);
            Assert.AreEqual(0, codeBlock.Text.Count);
        }

        [Test]
        public void TestParseCodeInlineNested()
        {
            string xml = @"<doc><c>oh no it's <c>another code</c></c></doc>";
            var element = MakeElement(xml);
            var block = parser.ParseCommentBlock(element);
            var outerCode = block.Blocks[0];
            var innerCode = outerCode.Blocks[0];

            Assert.AreEqual(CommentNodeType.CodeInline, block.Nodes[0].Type);
            Assert.AreEqual(CommentNodeType.Text, outerCode.Nodes[0].Type);
            Assert.AreEqual(CommentNodeType.CodeInline, outerCode.Nodes[1].Type);
            Assert.AreEqual(CommentNodeType.Text, innerCode.Nodes[0].Type);
            Assert.AreEqual("oh no it's ", outerCode.Text[0]);
            Assert.AreEqual("another code", innerCode.Text[0]);
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