using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace NADS.Comments
{
    public interface IAssemblyCommentParser
    {
        AssemblyComments Parse(Stream stream);
        AssemblyComments Parse(string xml);
        AssemblyComments Parse(XmlDocument xml);

        string ParseAssemblyName(XmlElement assemblyNode);
        IReadOnlyList<MemberComments> ParseMembers(XmlElement membersNode);
        MemberComments ParseMember(XmlElement memberNode);
        ParamComments ParseParam(XmlElement paramNode);
        CommentBlock ParseCommentBlock(XmlNode blockNode);
    }
}