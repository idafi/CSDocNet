using CSDocNet.Comments;

namespace CSDocNet.Markdown
{
    public interface IMDDocWriter
    {
        void WriteCommentBlock(in CommentBlock block);

        void WriteText(string text);
        void WriteCRef(string cRef);
        void WriteParamRef(string pRef);
        void WriteTypeParamRef(string tpRef);

        void WriteParagraph(in CommentBlock para);
        void WriteCodeBlock(in CommentBlock code);
        void WriteCodeInline(in CommentBlock code);

        void WriteList(in CommentList list);
    }
}