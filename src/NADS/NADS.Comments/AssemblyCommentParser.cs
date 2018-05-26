using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
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

                string assemblyName = ParseAssemblyName(doc["assembly"]);

                Log.Note("...done parsing XML document.");
                return new AssemblyComments(assemblyName,
                    null, null, null, null, null);
            }
            else
            { Log.Failure("Couldn't parse XML document.\n\tDocument is null"); }

            return AssemblyComments.Empty;
        }

        public string ParseAssemblyName(XmlElement assemblyNode)
        {
            if(!TryFindElement(assemblyNode, "name", out var name))
            { return ""; }

            return name.InnerText;
        }

        public IReadOnlyList<MemberComments> ParseMembers(XmlElement membersNode)
        {
            throw new NotImplementedException();
        }

        public MemberComments ParseMember(XmlElement memberNode)
        {
            throw new NotImplementedException();
        }

        public ParamComments ParseParam(XmlElement paramNode)
        {
            throw new NotImplementedException();
        }

        public CommentBlock ParseCommentBlock(XmlNode blockNode)
        {
            throw new NotImplementedException();
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
    }
}