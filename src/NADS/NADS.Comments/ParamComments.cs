namespace NADS.Comments
{
    public readonly struct ParamComments
    {
        public readonly string Name;
        public readonly CommentBlock Description;

        public ParamComments(string name, CommentBlock description)
        {
            Name = name ?? "";
            Description = description;
        }
    }
}