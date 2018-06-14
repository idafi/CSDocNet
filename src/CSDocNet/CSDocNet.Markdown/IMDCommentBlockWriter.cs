using CSDocNet.Comments;

namespace CSDocNet.Markdown
{
    public interface IMDCommentBlockWriter
    {
        string WriteCommentBlock(CommentBlock block);

        string WriteText(string text);
        string WriteCRef(string cRef);
        string WriteParamRef(string pRef);
        string WriteTypeParamRef(string tpRef);

        string WriteParagraph(CommentBlock para);
        string WriteCodeBlock(CommentBlock code);
        string WriteCodeInline(CommentBlock code);

        string WriteList(CommentList list);
    }
}