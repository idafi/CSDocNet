using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace CSDocNet.Comments
{
    public interface IAssemblyCommentParser
    {
        AssemblyComments Parse(Stream stream);
        AssemblyComments Parse(string xml);
        AssemblyComments Parse(XmlDocument xml);

        string ParseAssemblyName(XmlElement assemblyNode);
        IReadOnlyDictionary<string, MemberComments> ParseMembers(XmlElement membersNode);
        MemberComments ParseMember(XmlElement memberNode);
        ParamComments ParseParam(XmlElement paramNode, string nameAttribute);
        CommentBlock ParseCommentBlock(XmlNode blockNode);
    }
}