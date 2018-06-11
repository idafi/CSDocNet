namespace CSDocNet.Comments
{
    public readonly struct ParamComments
    {
        public readonly string Name;
        public readonly CommentBlock Description;

        public ParamComments(string name, in CommentBlock description)
        {
            Name = name ?? "";
            Description = description;
        }

        public static ParamComments Empty => new ParamComments(null, CommentBlock.Empty);
    }
}