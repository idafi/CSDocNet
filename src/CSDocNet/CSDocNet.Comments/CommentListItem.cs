namespace CSDocNet.Comments
{
    public class CommentListItem
    {
        public readonly CommentBlock Term;
        public readonly CommentBlock Description;

        public CommentListItem(CommentBlock term, CommentBlock description)
        {
            Term = term;
            Description = description;
        }

        public static CommentListItem Empty
            => new CommentListItem(CommentBlock.Empty, CommentBlock.Empty);
    }
}