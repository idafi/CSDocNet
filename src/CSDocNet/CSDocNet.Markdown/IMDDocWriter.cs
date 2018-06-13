using CSDocNet.Comments;

namespace CSDocNet.Markdown
{
    public interface IMDDocWriter
    {
        void WriteCommentBlock(CommentBlock block);

        void WriteText(string text);
        void WriteCRef(string cRef);
        void WriteParamRef(string pRef);
        void WriteTypeParamRef(string tpRef);

        void WriteParagraph(CommentBlock para);
        void WriteCodeBlock(CommentBlock code);
        void WriteCodeInline(CommentBlock code);

        void WriteList(CommentList list);
    }
}