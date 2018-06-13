namespace CSDocNet.Comments
{
    public class ParamComments
    {
        public readonly string Name;
        public readonly CommentBlock Description;

        public ParamComments(string name, CommentBlock description)
        {
            Name = name ?? "";
            Description = description;
        }

        public static ParamComments Empty => new ParamComments(null, CommentBlock.Empty);
    }
}