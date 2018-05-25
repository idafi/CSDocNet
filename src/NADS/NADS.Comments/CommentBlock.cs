using System.Collections.Generic;

namespace NADS.Comments
{
    public readonly struct CommentBlock
    {
        public readonly IReadOnlyList<CommentNode> Nodes;

        public readonly IReadOnlyList<string> Text;
        public readonly IReadOnlyList<CommentBlock> Blocks;

        public CommentBlock(IReadOnlyList<CommentNode> nodes,
            IReadOnlyList<string> text, IReadOnlyList<CommentBlock> blocks)
        {
            Nodes = nodes ?? new CommentNode[0];
            Text = text ?? new string[0];
            Blocks = blocks ?? new CommentBlock[0];
        }
    }
}