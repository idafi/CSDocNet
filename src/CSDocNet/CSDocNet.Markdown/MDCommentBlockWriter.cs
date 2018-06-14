using System.IO;
using System.Text.RegularExpressions;
using CSDocNet.Comments;

namespace CSDocNet.Markdown
{
    public class MDCommentBlockWriter : IMDCommentBlockWriter
    {
        volatile bool writeCode;
        
        public string WriteCommentBlock(CommentBlock block)
        {
            string str = "";

            foreach(CommentNode node in block.Nodes)
            { str += WriteCommentNode(block, node); }

            return str;
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
            string str = "\n\n";

            if(writeCode)
            { str += '\t'; }
            
            str += WriteCommentBlock(para);
            str += "\n\n";

            return str;
        }

        public string WriteCodeBlock(CommentBlock code)
        {
            writeCode = true;
            string str = WriteParagraph(code);
            writeCode = false;

            return str;
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

        string WriteCommentNode(CommentBlock block, CommentNode node)
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
                    return WriteParagraph(block.Blocks[node.ValueIndex]);
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