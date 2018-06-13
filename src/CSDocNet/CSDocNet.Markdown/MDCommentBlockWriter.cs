using System.IO;
using System.Text.RegularExpressions;
using CSDocNet.Comments;

namespace CSDocNet.Markdown
{
    public class MDCommentBlockWriter : IMDCommentBlockWriter
    {
        readonly TextWriter writer;
        bool writeCode;

        public MDCommentBlockWriter(TextWriter writer)
        {
            Check.Ref(writer);

            this.writer = writer;
        }

        public void WriteCommentBlock(CommentBlock block)
        {
            foreach(CommentNode node in block.Nodes)
            {
                switch(node.Type)
                {
                    case CommentNodeType.Text:
                        WriteText(block.Text[node.ValueIndex]);
                        break;
                    case CommentNodeType.CRef:
                        WriteCRef(block.Text[node.ValueIndex]);
                        break;
                    case CommentNodeType.ParamRef:
                        WriteParamRef(block.Text[node.ValueIndex]);
                        break;
                    case CommentNodeType.TypeParamRef:
                        WriteTypeParamRef(block.Text[node.ValueIndex]);
                        break;
                    case CommentNodeType.Paragraph:
                        WriteParagraph(block.Blocks[node.ValueIndex]);
                        break;
                    case CommentNodeType.CodeBlock:
                        WriteCodeBlock(block.Blocks[node.ValueIndex]);
                        break;
                    case CommentNodeType.CodeInline:
                        WriteCodeInline(block.Blocks[node.ValueIndex]);
                        break;
                    case CommentNodeType.List:
                        WriteList(block.Lists[node.ValueIndex]);
                        break;
                }
            }
        }

        public void WriteText(string text)
        {
            WriteSanitized(text);
        }

        public void WriteCRef(string cRef)
        {
            writer.Write('[');
            WriteSanitized(cRef);
            writer.Write(']');
            writer.Write('(');
            WriteSanitized(cRef);
            writer.Write(')');
        }

        public void WriteParamRef(string pRef)
        {
            writer.Write('`');
            WriteSanitized(pRef);
            writer.Write('`');
        }

        public void WriteTypeParamRef(string tpRef)
        {
            writer.Write('`');
            WriteSanitized(tpRef);
            writer.Write('`');
        }

        public void WriteParagraph(CommentBlock para)
        {
            writer.Write("\n\n");

            if(writeCode)
            { writer.Write("\t"); }
            
            WriteCommentBlock(para);
            writer.Write("\n\n");
        }

        public void WriteCodeBlock(CommentBlock code)
        {
            writeCode = true;
            WriteParagraph(code);
            writeCode = false;
        }

        public void WriteCodeInline(CommentBlock code)
        {
            writer.Write('`');
            WriteCommentBlock(code);
            writer.Write('`');
        }

        public void WriteList(CommentList list)
        {
            writer.Write("\n\n");

            switch(list.Type)
            {
                case CommentListType.Bullet:
                    foreach(CommentListItem item in list.Items)
                    {
                        writer.Write("- ");
                        WriteCommentBlock(item.Description);
                        writer.Write('\n');
                    }
                    break;
                case CommentListType.Number:
                    for(int i = 0; i < list.Items.Count; i++)
                    {
                        writer.Write($"{i + 1}. ");
                        WriteCommentBlock(list.Items[i].Description);
                        writer.Write('\n');
                    }
                    break;
            }

            writer.Write('\n');
        }

        void WriteSanitized(string text)
        {
            if(!string.IsNullOrEmpty(text))
            {
                text = text.Replace("`", "\\`");
                text = Regex.Replace(text, @"\s+", " ");
                writer.Write(text);
            }
        }
    }
}