namespace NADS.Comments
{
    public readonly struct CommentNode
    {
        public readonly CommentNodeType Type;
        public readonly int ValueIndex;

        public CommentNode(CommentNodeType type, int valueIndex)
        {
            Type = type;
            ValueIndex = valueIndex;
        }
    }
}