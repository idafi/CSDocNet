using System.IO;
using System.Text.RegularExpressions;
using CSDocNet.Comments;

namespace CSDocNet.Markdown
{
    public class MDCommentBlockWriter : IMDCommentBlockWriter
    {
        public string WriteCommentBlock(CommentBlock block)
        {
            return WriteCommentBlock(block, false);
        }

        public string WriteText(string text)
        {
            return WriteSanitized(text);
        }

        public string WriteCRef(string cRef)
        {
            return $"[{cRef}]({cRef})";
        }

        public string WriteParamRef(string pRef)
        {
            return $"`{pRef}`";
        }

        public string WriteTypeParamRef(string tpRef)
        {
            return $"`{tpRef}`";
        }

        public string WriteParagraph(CommentBlock para)
        {
            return WriteParagraph(para, false);
        }

        public string WriteCodeBlock(CommentBlock code)
        {
            return WriteParagraph(code, true);
        }

        public string WriteCodeInline(CommentBlock code)
        {
            return $"`{WriteCommentBlock(code)}`";
        }

        public string WriteList(CommentList list)
        {
            string str = "\n\n";

            switch(list.Type)
            {
                case CommentListType.Bullet:
                    foreach(CommentListItem item in list.Items)
                    { str += $"- {WriteCommentBlock(item.Description)}\n"; }
                    break;
                case CommentListType.Number:
                    for(int i = 0; i < list.Items.Count; i++)
                    { str += $"{i + 1}. {WriteCommentBlock(list.Items[i].Description)}\n"; }
                    break;
            }

            str += '\n';
            return str;
        }
        
        string WriteCommentBlock(CommentBlock block, bool writeCode)
        {
            string str = "";

            foreach(CommentNode node in block.Nodes)
            { str += WriteCommentNode(block, node, writeCode); }

            return str;
        }

        string WriteCommentNode(CommentBlock block, CommentNode node, bool writeCode)
        {
            switch(node.Type)
            {
                case CommentNodeType.Text:
                    return WriteText(block.Text[node.ValueIndex]);
                case CommentNodeType.CRef:
                    return WriteCRef(block.Text[node.ValueIndex]);
                case CommentNodeType.ParamRef:
                    return WriteParamRef(block.Text[node.ValueIndex]);
                case CommentNodeType.TypeParamRef:
                    return WriteTypeParamRef(block.Text[node.ValueIndex]);
                case CommentNodeType.Paragraph:
                    return WriteParagraph(block.Blocks[node.ValueIndex], writeCode);
                case CommentNodeType.CodeBlock:
                    return WriteCodeBlock(block.Blocks[node.ValueIndex]);
                case CommentNodeType.CodeInline:
                    return WriteCodeInline(block.Blocks[node.ValueIndex]);
                case CommentNodeType.List:
                    return WriteList(block.Lists[node.ValueIndex]);
                default:
                    return "";
            }
        }

        string WriteParagraph(CommentBlock para, bool writeCode)
        {
            string str = "\n\n";

            if(writeCode)
            { str += '\t'; }
            
            str += WriteCommentBlock(para, writeCode);
            str += "\n\n";

            return str;
        }

        string WriteSanitized(string text)
        {
            if(string.IsNullOrEmpty(text))
            { return ""; }

            text = text.Replace("`", "\\`");
            text = Regex.Replace(text, @"\s+", " ");
            return text;
        }
    }
}