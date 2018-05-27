using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using NADS.Collections;
using NADS.Debug;
using NADS.Logging;

namespace NADS.Comments
{
    public class AssemblyCommentParser : IAssemblyCommentParser
    {
        public AssemblyComments Parse(Stream stream)
        {
            if(stream != null)
            {
                try
                {
                    Log.Debug("Loading XML document from stream...");

                    XmlDocument xml = new XmlDocument();
                    xml.Load(stream);

                    Log.Debug("...done loading XML document.");
                    return Parse(xml);
                }
                catch(Exception e)
                { Log.Failure("Couldn't read XML stream.\n\t" + e); }
            }
            else
            { Log.Failure("Couldn't read XML stream.\n\tStream is null"); }

            return AssemblyComments.Empty;
        }

        public AssemblyComments Parse(string xml)
        {
            if(xml != null)
            {
                try
                {
                    Log.Debug("Loading XML document from string data...");
                    
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(xml);

                    Log.Debug("...done loading XML document.");
                    return Parse(xmlDoc);
                }
                catch(Exception e)
                { Log.Failure("Couldn't read XML data.\n\t" + e); }
            }
            else
            { Log.Failure("Couldn't read XML data.\n\tString is null"); }

            return AssemblyComments.Empty;
        }
        
        public AssemblyComments Parse(XmlDocument xml)
        {
            if(xml != null)
            {
                Log.Note("Parsing XML document...");

                if(!TryFindElement(xml, "doc", out var doc, LogLevel.Failure))
                { return AssemblyComments.Empty; }

                string assemblyName = TryFindElement(doc, "assembly", out var assemblyNode)
                    ? ParseAssemblyName(assemblyNode)
                    : "";

                var members = ParseMembers(doc["members"]);

                Log.Note("...done parsing XML document.");
                return new AssemblyComments(assemblyName, members);
            }
            else
            { Log.Failure("Couldn't parse XML document.\n\tDocument is null"); }

            return AssemblyComments.Empty;
        }

        public string ParseAssemblyName(XmlElement assemblyNode)
        {
            if(assemblyNode == null)
            { return ""; }

            if(!TryFindElement(assemblyNode, "name", out var name))
            { return ""; }

            return name.InnerText;
        }

        public IReadOnlyList<MemberComments> ParseMembers(XmlElement membersNode)
        {
            if(membersNode == null)
            { return Empty<MemberComments>.EmptyList; }

            return ParseChildren(membersNode, "member", ParseMember);
        }

        public MemberComments ParseMember(XmlElement memberNode)
        {
            if(memberNode == null)
            { return MemberComments.Empty; }

            TryFindAttributeValue(memberNode, "name", out string name);

            var summary = ParseCommentBlock(memberNode["summary"]);
            var remarks = ParseCommentBlock(memberNode["remarks"]);
            var value = ParseCommentBlock(memberNode["value"]);
            var returns = ParseCommentBlock(memberNode["returns"]);
            var example = ParseCommentBlock(memberNode["example"]);

            var parameters = ParseChildren(memberNode, "param", n => ParseParam(n, "name"));
            var typeParams = ParseChildren(memberNode, "typeparam", n => ParseParam(n, "name"));
            var exceptions = ParseChildren(memberNode, "exception", n => ParseParam(n, "cref"));
            var permissions = ParseChildren(memberNode, "permission", n => ParseParam(n, "cref"));

            bool inheritDoc = TryFindElement(memberNode, "inheritdoc", out var _, LogLevel.Debug);

            return new MemberComments(name, summary, remarks, value, returns, example,
                parameters, typeParams, exceptions, permissions, inheritDoc);
        }

        public ParamComments ParseParam(XmlElement paramNode, string nameAttribute)
        {
            if(paramNode == null || nameAttribute == null)
            { return ParamComments.Empty; }

            TryFindAttributeValue(paramNode, nameAttribute, out string name);
            return new ParamComments(name, ParseCommentBlock(paramNode));
        }

        public CommentBlock ParseCommentBlock(XmlNode blockNode)
        {
            if(blockNode == null)
            { return CommentBlock.Empty; }

            int nodeCt = blockNode.ChildNodes.Count;
            var nodes = new List<CommentNode>(nodeCt);
            var text = new List<string>(nodeCt);
            var blocks = new List<CommentBlock>(nodeCt);
            var lists = new List<CommentList>(nodeCt);

            foreach(XmlNode child in blockNode.ChildNodes)
            {
                switch(child.Name)
                {
                    case "#text":
                        ParseText(child, nodes, text);
                        break;
                    case "see":
                        ParseCRef(child, nodes, text);
                        break;
                    case "paramref":
                        ParseParamRef(child, nodes, text);
                        break;
                    case "typeparamref":
                        ParseTypeParamRef(child, nodes, text);
                        break;
                    case "para":
                        ParseParagraph(child, nodes, blocks);
                        break;
                    case "code":
                        ParseCodeBlock(child, nodes, blocks);
                        break;
                    case "c":
                        ParseCodeInline(child, nodes, blocks);
                        break;
                    case "list":
                        ParseList(child, nodes, lists);
                        break;
                    default:
                        Log.Warning($"Unrecognized child node type: '{child.Name}'");
                        break;
                }
            }

            return new CommentBlock(nodes.ToArray(), text.ToArray(), blocks.ToArray(), lists.ToArray());
        }

        bool TryFindElement(XmlNode parent, string child, out XmlElement element,
            LogLevel failureLevel = LogLevel.Warning)
        {
            Assert.Ref(parent, child);

            element = parent[child];
            if(element == null)
            { Log.Write(failureLevel, $"Couldn't find '{child}' element in '{parent.Name}' node."); }

            return (element != null);
        }

        bool TryFindAttributeValue(XmlNode node, string attribute, out string value,
            LogLevel failureLevel = LogLevel.Warning)
        {
            Assert.Ref(node, attribute);

            var attr = node.Attributes[attribute];
            if(attr == null)
            { Log.Write(failureLevel, $"Couldn't find '{attribute}' attribute on '{node.Name}' node."); }

            value = attr?.Value ?? "";
            return (attr != null);
        }

        IReadOnlyList<T> ParseChildren<T>(XmlElement parentNode, string tag, Func<XmlElement, T> parser)
        {
            Assert.Ref(parentNode, tag, parser);

            XmlNodeList childList = parentNode.GetElementsByTagName(tag);
            var children = new List<T>(childList?.Count ?? 0);

            if(childList != null)
            {
                foreach(XmlElement childNode in childList)
                { children.Add(parser(childNode)); }
            }

            return children.ToArray();
        }

        void ParseText(XmlNode xmlNode, List<CommentNode> nodes, List<string> text)
        {
            Assert.Ref(xmlNode, nodes, text);

            nodes.Add(new CommentNode(CommentNodeType.Text, text.Count));
            text.Add(xmlNode.InnerText);
        }

        void ParseCRef(XmlNode xmlNode, List<CommentNode> nodes, List<string> text)
        {
            Assert.Ref(xmlNode, nodes, text);

            if(TryFindAttributeValue(xmlNode, "cref", out string value))
            {
                nodes.Add(new CommentNode(CommentNodeType.CRef, text.Count));
                text.Add(value);
            }
        }

        void ParseParamRef(XmlNode xmlNode, List<CommentNode> nodes, List<string> text)
        {
            Assert.Ref(xmlNode, nodes, text);

            if(TryFindAttributeValue(xmlNode, "name", out string value))
            {
                nodes.Add(new CommentNode(CommentNodeType.ParamRef, text.Count));
                text.Add(value);
            }
        }

        void ParseTypeParamRef(XmlNode xmlNode, List<CommentNode> nodes, List<string> text)
        {
            Assert.Ref(xmlNode, nodes, text);

            if(TryFindAttributeValue(xmlNode, "name", out string value))
            {
                nodes.Add(new CommentNode(CommentNodeType.TypeParamRef, text.Count));
                text.Add(value);
            }
        }

        void ParseParagraph(XmlNode xmlNode, List<CommentNode> nodes, List<CommentBlock> blocks)
        {
            Assert.Ref(xmlNode, nodes, blocks);

            nodes.Add(new CommentNode(CommentNodeType.Paragraph, blocks.Count));
            blocks.Add(ParseCommentBlock(xmlNode));
        }

        void ParseCodeBlock(XmlNode xmlNode, List<CommentNode> nodes, List<CommentBlock> blocks)
        {
            Assert.Ref(xmlNode, nodes, blocks);

            nodes.Add(new CommentNode(CommentNodeType.CodeBlock, blocks.Count));
            blocks.Add(ParseCommentBlock(xmlNode));
        }

        void ParseCodeInline(XmlNode xmlNode, List<CommentNode> nodes, List<CommentBlock> blocks)
        {
            Assert.Ref(xmlNode, nodes, blocks);

            nodes.Add(new CommentNode(CommentNodeType.CodeInline, blocks.Count));
            blocks.Add(ParseCommentBlock(xmlNode));
        }

        void ParseList(XmlNode xmlNode, List<CommentNode> nodes, List<CommentList> lists)
        {
            Assert.Ref(xmlNode, nodes, lists);

            if(xmlNode is XmlElement e)
            {
                var type = ParseListType(xmlNode);
                var header = TryFindElement(xmlNode, "listheader", out var headerNode, LogLevel.Debug)
                    ? ParseListItem(headerNode)
                    : CommentListItem.Empty;
                var items = ParseChildren(e, "item", ParseListItem);

                nodes.Add(new CommentNode(CommentNodeType.List, lists.Count));
                lists.Add(new CommentList(type, header, items));
            }
            else
            { Log.Warning("Invalid list node: expected an XML element"); }
        }

        CommentListType ParseListType(XmlNode listNode)
        {
            Assert.Ref(listNode);

            if(!TryFindAttributeValue(listNode, "type", out string value))
            { return default; }

            switch(value)
            {
                case "bullet":
                    return CommentListType.Bullet;
                case "number":
                    return CommentListType.Number;
                case "table":
                    return CommentListType.Table;
                default:
                    Log.Warning($"Unrecognized list type: '{value}'");
                    return default;
            }
        }

        CommentListItem ParseListItem(XmlElement itemNode)
        {
            Assert.Ref(itemNode);

            CommentBlock term = TryFindElement(itemNode, "term", out var termNode, LogLevel.Debug)
                ? ParseCommentBlock(termNode)
                : CommentBlock.Empty;
            CommentBlock description = TryFindElement(itemNode, "description", out var descNode, LogLevel.Debug)
                ? ParseCommentBlock(descNode)
                : ParseCommentBlock(itemNode);
            
            return new CommentListItem(term, description);
        }
    }
}