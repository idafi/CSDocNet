namespace NADS.Comments
{
    public readonly struct CommentListItem
    {
        public readonly CommentBlock Term;
        public readonly CommentBlock Description;

        public CommentListItem(in CommentBlock term, in CommentBlock description)
        {
            Term = term;
            Description = description;
        }
    }
}